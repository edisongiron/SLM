using Microsoft.Extensions.DependencyInjection;
using ServindAp.Application.DependencyInjection;
using ServindAp.Infrastructure.DependencyInjection;
using ServindAp.UI.Forms;
using System.Runtime.InteropServices;


namespace ServindAp.UI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            SetAppUserModelID("ServindAp.UI");

            ApplicationConfiguration.Initialize();

            var services = new ServiceCollection();

            var dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "servindap.db");
            var connectionString = $"Data Source={dbPath}";

            services.AddInfrastructure(connectionString);
            services.AddApplication();

            services.AddTransient<Form1>();

            var serviceProvider = services.BuildServiceProvider();

            InfrastructureServiceCollectionExtensions.InitializeDatabase(serviceProvider);

            var mainForm = serviceProvider.GetRequiredService<Form1>();

            mainForm.Text = "Servind";
            mainForm.Icon = new Icon(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "icon.ico")
            );
            mainForm.ShowIcon = true;
            mainForm.ShowInTaskbar = true;

            System.Windows.Forms.Application.Run(mainForm);

        }

        internal static class NativeMethods
        {
            [DllImport("shell32.dll", SetLastError = true)]
            internal static extern int SetCurrentProcessExplicitAppUserModelID(string AppID);
        }

        internal static void SetAppUserModelID(string appId)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                NativeMethods.SetCurrentProcessExplicitAppUserModelID(appId);
            }
        }
    }
}