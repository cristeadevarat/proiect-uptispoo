using InchirieriMasini.Infrastructure;
using InchirieriMasini.Interfaces;
using InchirieriMasini.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InchirieriMasini;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // Configure .NET Core GenericHost with dependency injection
        var host = CreateHostBuilder().Build();

        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        
        // Get the main form from DI container
        var mainForm = host.Services.GetRequiredService<MainForm>();
        Application.Run(mainForm);
    }

    static IHostBuilder CreateHostBuilder()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                // Register logging
                services.AddLogging(configure =>
                {
                    configure.AddConsole();
                    configure.AddDebug();
                    configure.SetMinimumLevel(LogLevel.Information);
                });

                // Register wrappers (external dependencies isolation)
                services.AddSingleton<IFileStorage, JsonFileStorage>();
                services.AddSingleton<IConsoleWrapper, ConsoleWrapper>();

                // Register services
                services.AddSingleton<VehicleService>();
                services.AddSingleton<CustomerService>();
                services.AddSingleton<RentalService>();

                // Register WinForms
                services.AddTransient<MainForm>();
            });
    }
}