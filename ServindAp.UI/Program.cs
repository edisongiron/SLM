using Microsoft.Extensions.DependencyInjection;
using ServindAp.Application.DependencyInjection;
using ServindAp.Infrastructure.DependencyInjection;
using ServindAp.UI.Forms;

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
            System.Windows.Forms.Application.Run(mainForm);
        }
    }
}