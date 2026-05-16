using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Tiferet.Avalonia.Blueprints;
using Tiferet.Avalonia.Examples.Calculator.Contexts;

namespace Tiferet.Avalonia.Examples.Calculator;

public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Bootstrap Tiferet with the calculator interface.
            var configDir = Path.Combine(AppContext.BaseDirectory, "app", "assets");
            var options = new TiferetAvaloniaOptions
            {
                InterfaceId = "calculator",
                ConfigDir = configDir,
            };
            var appContext = AvaloniaBlueprint.BuildApp(options);

            // Create the view context and main window.
            var viewContext = new CalculatorViewContext(appContext);
            desktop.MainWindow = new MainWindow
            {
                DataContext = viewContext,
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
