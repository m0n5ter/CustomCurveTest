using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Styling;

namespace CustomCurve.InputRotation.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnThemeClick(object? sender, RoutedEventArgs e)
    {
        RequestedThemeVariant = ActualThemeVariant == ThemeVariant.Dark ? ThemeVariant.Light : ThemeVariant.Dark;
    }
}