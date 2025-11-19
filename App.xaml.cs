using System.Configuration;
using System.Data;
using System.Windows;

namespace pr7_trpo_1_KMA
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ThemeHelper.ApplySaved();
        }
    }

}
