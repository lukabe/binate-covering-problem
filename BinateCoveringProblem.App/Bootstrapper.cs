using BinateCoveringProblem.App.Shell;
using BinateCoveringProblem.App.Shell.Matrix;
using Caliburn.Micro;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace BinateCoveringProblem.App
{
    public class Bootstrapper : BootstrapperBase
    {
        private IServiceProvider serviceProvider;

        public Bootstrapper()
        {
            Initialize();
        }

        protected override object GetInstance(Type service, string key)
        {
            return serviceProvider.GetService(service);
        }

        private static void Configure(IServiceCollection services)
        {
            services.AddSingleton<IWindowManager, WindowManager>();
            services.AddSingleton<ShellViewModel>();
            services.AddSingleton<IMatrixViewModel, MatrixViewModel>();
            services.AddSingleton<IMatrixRepresentation, MatrixRepresentation>();
        }

        protected override void Configure()
        {
            base.Configure();

            var services = new ServiceCollection();
            Configure(services);

            serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
