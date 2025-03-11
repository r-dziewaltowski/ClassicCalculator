using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ClassicCalculatorBlazorApp;
using ClassicCalculator;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<ICalculator, Calculator>(provider => new Calculator(displayLength: 10));

// Configure logging
builder.Logging.SetMinimumLevel(LogLevel.Information);

var host = builder.Build();

// Global exception handling
AppDomain.CurrentDomain.UnhandledException += (sender, eventArgs) =>
{
    var exception = eventArgs.ExceptionObject as Exception;
    var logger = host.Services.GetRequiredService<ILogger<Program>>();
    logger.LogError(exception, "Unhandled exception occurred.");
};

await host.RunAsync();
