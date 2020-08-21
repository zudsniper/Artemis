﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Artemis.Core.Events;
using Artemis.Core.Services.Interfaces;
using Artemis.UI.Events;
using Artemis.UI.Ninject.Factories;
using Artemis.UI.Screens.Home;
using Artemis.UI.Screens.News;
using Artemis.UI.Screens.Settings;
using Artemis.UI.Screens.SurfaceEditor;
using Artemis.UI.Screens.Workshop;
using MaterialDesignExtensions.Controls;
using MaterialDesignExtensions.Model;
using MaterialDesignThemes.Wpf;
using Ninject;
using Stylet;

namespace Artemis.UI.Screens.Sidebar
{
    public class SidebarViewModel : PropertyChangedBase, IHandle<RequestSelectSidebarItemEvent>, IDisposable
    {
        private readonly IKernel _kernel;
        private readonly IModuleVmFactory _moduleVmFactory;
        private readonly IPluginService _pluginService;
        private BindableCollection<INavigationItem> _sidebarItems;
        private Dictionary<INavigationItem, Core.Plugins.Modules.Module> _sidebarModules;
        private IScreen _selectedItem;
        private bool _isSidebarOpen;
        private readonly Timer _activeModulesUpdateTimer;
        private string _activeModules;

        public SidebarViewModel(IKernel kernel, IEventAggregator eventAggregator, IModuleVmFactory moduleVmFactory, IPluginService pluginService)
        {
            _kernel = kernel;
            _moduleVmFactory = moduleVmFactory;
            _pluginService = pluginService;

            SidebarModules = new Dictionary<INavigationItem, Core.Plugins.Modules.Module>();
            SidebarItems = new BindableCollection<INavigationItem>();

            _activeModulesUpdateTimer = new Timer(1000);
            _activeModulesUpdateTimer.Start();
            _activeModulesUpdateTimer.Elapsed += ActiveModulesUpdateTimerOnElapsed;

            _pluginService.PluginEnabled += PluginServiceOnPluginEnabled;
            _pluginService.PluginDisabled += PluginServiceOnPluginDisabled;

            SetupSidebar();
            eventAggregator.Subscribe(this);
        }

        private void ActiveModulesUpdateTimerOnElapsed(object sender, EventArgs e)
        {
            if (!IsSidebarOpen)
                return;

            var activeModules = SidebarModules.Count(m => m.Value.IsActivated);
            ActiveModules = activeModules == 1 ? "1 active module" : $"{activeModules} active modules";
        }

        public BindableCollection<INavigationItem> SidebarItems
        {
            get => _sidebarItems;
            set => SetAndNotify(ref _sidebarItems, value);
        }

        public Dictionary<INavigationItem, Core.Plugins.Modules.Module> SidebarModules
        {
            get => _sidebarModules;
            set => SetAndNotify(ref _sidebarModules, value);
        }

        public string ActiveModules
        {
            get => _activeModules;
            set => SetAndNotify(ref _activeModules, value);
        }

        public IScreen SelectedItem
        {
            get => _selectedItem;
            set => SetAndNotify(ref _selectedItem, value);
        }

        public bool IsSidebarOpen
        {
            get => _isSidebarOpen;
            set
            {
                SetAndNotify(ref _isSidebarOpen, value);
                if (value)
                    ActiveModulesUpdateTimerOnElapsed(this, EventArgs.Empty);
            }
        }

        public void SetupSidebar()
        {
            SidebarItems.Clear();
            SidebarModules.Clear();

            // Add all default sidebar items
            SidebarItems.Add(new FirstLevelNavigationItem {Icon = PackIconKind.Home, Label = "Home"});
            SidebarItems.Add(new FirstLevelNavigationItem {Icon = PackIconKind.Newspaper, Label = "News"});
            SidebarItems.Add(new FirstLevelNavigationItem {Icon = PackIconKind.TestTube, Label = "Workshop"});
            SidebarItems.Add(new FirstLevelNavigationItem {Icon = PackIconKind.Edit, Label = "Surface Editor"});
            SidebarItems.Add(new FirstLevelNavigationItem {Icon = PackIconKind.Settings, Label = "Settings"});

            // Add all activated modules
            SidebarItems.Add(new DividerNavigationItem());
            SidebarItems.Add(new SubheaderNavigationItem {Subheader = "Modules"});
            var modules = _pluginService.GetPluginsOfType<Core.Plugins.Modules.Module>().ToList();
            foreach (var module in modules)
                AddModule(module);

            // Select the top item, which will be one of the defaults
            Task.Run(() => SelectSidebarItem(SidebarItems[0]));
        }

        // ReSharper disable once UnusedMember.Global - Called by view
        public async Task SelectItem(WillSelectNavigationItemEventArgs args)
        {
            if (args.NavigationItemToSelect == null)
            {
                SelectedItem = null;
                return;
            }

            await SelectSidebarItem(args.NavigationItemToSelect);
        }

        public void AddModule(Core.Plugins.Modules.Module module)
        {
            // Ensure the module is not already in the list
            if (SidebarModules.Any(io => io.Value == module))
                return;

            // Icon is provided as string to avoid having to reference MaterialDesignThemes
            var parsedIcon = Enum.TryParse<PackIconKind>(module.DisplayIcon, true, out var iconEnum);
            if (parsedIcon == false)
                iconEnum = PackIconKind.QuestionMarkCircle;
            var sidebarItem = new FirstLevelNavigationItem {Icon = iconEnum, Label = module.DisplayName};
            SidebarItems.Add(sidebarItem);
            SidebarModules.Add(sidebarItem, module);
        }

        public void RemoveModule(Core.Plugins.Modules.Module module)
        {
            // If not in the list there's nothing to do
            if (SidebarModules.All(io => io.Value != module))
                return;

            var existing = SidebarModules.First(io => io.Value == module);
            SidebarItems.Remove(existing.Key);
            SidebarModules.Remove(existing.Key);
        }

        private async Task SelectSidebarItem(INavigationItem sidebarItem)
        {
            // A module was selected if the dictionary contains the selected item
            if (SidebarModules.ContainsKey(sidebarItem))
                await ActivateModule(sidebarItem);
            else if (sidebarItem is FirstLevelNavigationItem navigationItem)
                await ActivateViewModel(navigationItem.Label);
            else if (await CloseCurrentItem())
                SelectedItem = null;
        }

        private async Task<bool> CloseCurrentItem()
        {
            if (SelectedItem == null)
                return true;

            var canClose = await SelectedItem.CanCloseAsync();
            if (!canClose)
                return false;

            SelectedItem.Close();
            return true;
        }

        private async Task ActivateViewModel(string label)
        {
            if (label == "Home")
                await ActivateViewModel<HomeViewModel>();
            else if (label == "News")
                await ActivateViewModel<NewsViewModel>();
            else if (label == "Workshop")
                await ActivateViewModel<WorkshopViewModel>();
            else if (label == "Surface Editor")
                await ActivateViewModel<SurfaceEditorViewModel>();
            else if (label == "Settings")
                await ActivateViewModel<SettingsViewModel>();
        }

        private async Task ActivateViewModel<T>()
        {
            if (await CloseCurrentItem())
                SelectedItem = (IScreen) _kernel.Get<T>();
        }

        private async Task ActivateModule(INavigationItem sidebarItem)
        {
            if (await CloseCurrentItem())
                SelectedItem = SidebarModules.ContainsKey(sidebarItem) ? _moduleVmFactory.Create(SidebarModules[sidebarItem]) : null;
        }

        #region Event handlers

        private void PluginServiceOnPluginEnabled(object sender, PluginEventArgs e)
        {
            if (e.PluginInfo.Instance is Core.Plugins.Modules.Module module)
                AddModule(module);
        }

        private void PluginServiceOnPluginDisabled(object sender, PluginEventArgs e)
        {
            if (e.PluginInfo.Instance is Core.Plugins.Modules.Module module)
                RemoveModule(module);
        }

        public void Handle(RequestSelectSidebarItemEvent message)
        {
            Execute.OnUIThread(async () => await ActivateViewModel(message.Label));
        }

        #endregion

        public void Dispose()
        {
            var closeTask = CloseCurrentItem();
            closeTask.Wait();

            _pluginService.PluginEnabled -= PluginServiceOnPluginEnabled;
            _pluginService.PluginDisabled -= PluginServiceOnPluginDisabled;

            _activeModulesUpdateTimer.Stop();
            _activeModulesUpdateTimer.Elapsed -= ActiveModulesUpdateTimerOnElapsed;
        }
    }
}