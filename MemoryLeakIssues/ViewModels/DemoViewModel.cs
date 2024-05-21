using CommunityToolkit.Mvvm.ComponentModel;
using Dock.Model.Mvvm.Controls;
using System.Diagnostics;

namespace MemoryLeakIssues.ViewModels;

[DebuggerDisplay("$DEMO [{Title}]")]
public partial class DemoViewModel : Document
{
    [ObservableProperty]
    private string _greeting = "Hello World";

    public DemoViewModel()
    {
        Title = "Demo View";
    }

    ~DemoViewModel()
    {

    }
}
