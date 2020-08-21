﻿using Artemis.Core.Models.Profile;
using Artemis.Core.Models.Profile.Conditions;
using Artemis.Core.Models.Profile.LayerProperties;
using Artemis.Core.Models.Profile.LayerProperties.Attributes;
using Artemis.Core.Models.Surface;
using Artemis.Core.Plugins;
using Artemis.Core.Plugins.Modules;
using Artemis.UI.Screens.Module;
using Artemis.UI.Screens.ProfileEditor;
using Artemis.UI.Screens.ProfileEditor.DisplayConditions;
using Artemis.UI.Screens.ProfileEditor.DisplayConditions.Abstract;
using Artemis.UI.Screens.ProfileEditor.LayerProperties;
using Artemis.UI.Screens.ProfileEditor.LayerProperties.Abstract;
using Artemis.UI.Screens.ProfileEditor.LayerProperties.LayerEffects;
using Artemis.UI.Screens.ProfileEditor.LayerProperties.Timeline;
using Artemis.UI.Screens.ProfileEditor.LayerProperties.Tree;
using Artemis.UI.Screens.ProfileEditor.ProfileTree.TreeItem;
using Artemis.UI.Screens.ProfileEditor.Visualization;
using Artemis.UI.Screens.ProfileEditor.Visualization.Tools;
using Artemis.UI.Screens.Settings.Debug;
using Artemis.UI.Screens.Settings.Tabs.Devices;
using Artemis.UI.Screens.Settings.Tabs.Plugins;
using Stylet;

namespace Artemis.UI.Ninject.Factories
{
    public interface IVmFactory
    {
    }

    public interface IModuleVmFactory : IVmFactory
    {
        ModuleRootViewModel Create(Module module);
    }

    public interface ISettingsVmFactory : IVmFactory
    {
        PluginSettingsViewModel CreatePluginSettingsViewModel(Plugin plugin);
        DeviceSettingsViewModel CreateDeviceSettingsViewModel(ArtemisDevice device);
    }

    public interface IDeviceDebugVmFactory : IVmFactory
    {
        DeviceDebugViewModel Create(ArtemisDevice device);
    }

    public interface IProfileEditorVmFactory : IVmFactory
    {
        ProfileEditorViewModel Create(ProfileModule module);
    }

    public interface IFolderVmFactory : IVmFactory
    {
        FolderViewModel Create(ProfileElement folder);
        FolderViewModel Create(TreeItemViewModel parent, ProfileElement folder);
    }

    public interface ILayerVmFactory : IVmFactory
    {
        LayerViewModel Create(TreeItemViewModel parent, ProfileElement folder);
    }

    public interface IProfileLayerVmFactory : IVmFactory
    {
        ProfileLayerViewModel Create(Layer layer, ProfileViewModel profileViewModel);
    }

    public interface IVisualizationToolVmFactory : IVmFactory
    {
        ViewpointMoveToolViewModel ViewpointMoveToolViewModel(ProfileViewModel profileViewModel);
        EditToolViewModel EditToolViewModel(ProfileViewModel profileViewModel);
        SelectionToolViewModel SelectionToolViewModel(ProfileViewModel profileViewModel);
        SelectionRemoveToolViewModel SelectionRemoveToolViewModel(ProfileViewModel profileViewModel);
    }
    public interface IDisplayConditionsVmFactory : IVmFactory
    {
        DisplayConditionGroupViewModel DisplayConditionGroupViewModel(DisplayConditionGroup displayConditionGroup, DisplayConditionViewModel parent, bool isListGroup);
        DisplayConditionListViewModel DisplayConditionListViewModel(DisplayConditionList displayConditionList, DisplayConditionViewModel parent);
        DisplayConditionPredicateViewModel DisplayConditionPredicateViewModel(DisplayConditionPredicate displayConditionPredicate, DisplayConditionViewModel parent);
        DisplayConditionListPredicateViewModel DisplayConditionListPredicateViewModel(DisplayConditionListPredicate displayConditionListPredicate, DisplayConditionViewModel parent);
    }

    public interface ILayerPropertyVmFactory : IVmFactory
    {
        LayerPropertyGroupViewModel LayerPropertyGroupViewModel(LayerPropertyGroup layerPropertyGroup, PropertyGroupDescriptionAttribute propertyGroupDescription);
        TreeViewModel TreeViewModel(LayerPropertiesViewModel layerPropertiesViewModel, BindableCollection<LayerPropertyGroupViewModel> layerPropertyGroups);
        EffectsViewModel EffectsViewModel(LayerPropertiesViewModel layerPropertiesViewModel);
        TimelineViewModel TimelineViewModel(LayerPropertiesViewModel layerPropertiesViewModel, BindableCollection<LayerPropertyGroupViewModel> layerPropertyGroups);
        TreePropertyGroupViewModel TreePropertyGroupViewModel(LayerPropertyBaseViewModel layerPropertyBaseViewModel);
        TimelinePropertyGroupViewModel TimelinePropertyGroupViewModel(LayerPropertyBaseViewModel layerPropertyBaseViewModel);
    }
}