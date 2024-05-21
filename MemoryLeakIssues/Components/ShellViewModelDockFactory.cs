using Dock.Avalonia.Controls;
using Dock.Model.Controls;
using Dock.Model.Core;
using Dock.Model.Mvvm;
using Dock.Model.Mvvm.Controls;
using MemoryLeakIssues.ViewModels;

namespace MemoryLeakIssues.Components;

public class ShellViewModelDockFactory : Factory
{
    public const string ROOT = "__root__";
    public const string LAYOUT = "__layout__";
    public const string DOCUMENTS = "__documents__";

    public IRootDock Root { get; }

    public DocumentDock Documents { get; } = new() {
        Id = DOCUMENTS
    };

    public ProportionalDock Layout { get; } = new() {
        Id = LAYOUT
    };

    public ShellViewModelDockFactory()
    {
        Root = CreateRootDock();
        Root.Id = ROOT;
    }

    public override IRootDock CreateLayout()
    {
        Documents.VisibleDockables = CreateList<IDockable>(new DemoViewModel {
            Title = "Home"
        });

        Layout.ActiveDockable = Documents;
        Layout.VisibleDockables = CreateList<IDockable>(Documents);

        Root.ActiveDockable = Layout;
        Root.DefaultDockable = Layout;
        Root.VisibleDockables = CreateList<IDockable>(Layout);

        return Root;
    }

    public override void InitLayout(IDockable layout)
    {
        DockableLocator = new Dictionary<string, Func<IDockable?>>() {
            [DOCUMENTS] = () => Documents,
            [LAYOUT] = () => Layout,
            [ROOT] = () => Root
        };

        HostWindowLocator = new() {
            [nameof(IDockWindow)] = () => new HostWindow()
        };

        base.InitLayout(layout);
    }
}
