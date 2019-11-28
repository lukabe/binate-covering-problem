using BinateCoveringProblem.App.Shell;
using Caliburn.Micro;
using System.Windows;

namespace BinateCoveringProblem.App
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
