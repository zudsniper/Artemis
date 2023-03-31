using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

namespace Artemis.UI.Screens.Settings;

public partial class ReleasesTabView : ReactiveUserControl<ReleasesTabViewModel>
{
    public ReleasesTabView()
    {
        InitializeComponent();
    }

}