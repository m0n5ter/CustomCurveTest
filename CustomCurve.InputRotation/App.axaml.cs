using ActiproSoftware.UI.Avalonia.Media;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CustomCurve.InputRotation.ViewModels;
using CustomCurve.InputRotation.Views;

namespace CustomCurve.InputRotation;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        ImageProvider.Default.ChromaticAdaptationMode = ImageChromaticAdaptationMode.Always;

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}