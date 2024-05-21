using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using MemoryLeakIssues.Components;
using MemoryLeakIssues.ViewModels;
using MemoryLeakIssues.Views;

namespace MemoryLeakIssues;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // Line below is needed to remove Avalonia data validation.
        // Without this line you will get duplicate validations from both Avalonia and CT
        BindingPlugins.DataValidators.RemoveAt(0);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new ShellView {
                DataContext = new ShellViewModel(
                    new ShellViewModelDockFactory()
                )
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
