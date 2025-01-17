using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

namespace Artemis.UI.Screens.Device;

public partial class DevicePropertiesTabView : ReactiveUserControl<DevicePropertiesTabViewModel>
{
    public DevicePropertiesTabView()
    {
        InitializeComponent();
    }


    private void InputElement_OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        ViewModel?.BrowseCustomLayout();
    }
}