using System.Reflection;
using System.Threading;
using System.Windows;

namespace FiveM_Launcher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Mutex _mutex = null;

        protected override void OnStartup(StartupEventArgs e)
        {
            bool createdNew;
            _mutex = new Mutex(true, Assembly.GetExecutingAssembly().GetName().Name, out createdNew);
            if (!createdNew)
            {
                Application.Current.Shutdown();
            }
            base.OnStartup(e);
        }

    }
}
