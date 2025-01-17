using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

namespace Artemis.UI.DefaultTypes.PropertyInput;

public partial class StringPropertyInputView : ReactiveUserControl<FloatPropertyInputViewModel>
{
    public StringPropertyInputView()
    {
        InitializeComponent();
        AddHandler(KeyUpEvent, OnRoutedKeyUp, handledEventsToo: true);
    }


    private void OnRoutedKeyUp(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter || e.Key == Key.Escape)
            FocusManager.Instance!.Focus(null);
    }
}