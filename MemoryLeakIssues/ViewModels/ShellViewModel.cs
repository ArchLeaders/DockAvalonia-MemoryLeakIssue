using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Dock.Model.Controls;
using MemoryLeakIssues.Components;

namespace MemoryLeakIssues.ViewModels;

public partial class ShellViewModel : ObservableObject
{
    private readonly ShellViewModelDockFactory _factory;
    private readonly IRootDock _layout;

    public IRootDock Layout => _layout;

    [RelayCommand]
    private Task Cleanup()
    {
        Layout.FocusedDockable = null;

        // _factory.Documents.VisibleDockables.Clear();
        // _factory.Layout.VisibleDockables.Clear();

        GC.Collect();
        GC.WaitForPendingFinalizers();

        return Task.CompletedTask;
    }

    public ShellViewModel(ShellViewModelDockFactory factory)
    {
        _factory = factory;
        _layout = factory.CreateLayout();
        factory.InitLayout(_layout);
        factory.AddDockable(factory.Documents, new DemoViewModel());
    }
}
