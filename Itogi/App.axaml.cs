using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Itogi.Windows.DepartmentsDir;
using Itogi.Windows.EmpsDir;
using Itogi.Windows.PostsDir;

namespace Itogi;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new RabStol();
        }

        base.OnFrameworkInitializationCompleted();
    }
}