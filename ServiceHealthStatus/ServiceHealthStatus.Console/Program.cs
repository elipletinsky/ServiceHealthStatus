
using ServiceHealthStatus.ViewModel;
using System;
using System.Collections;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ServiceHealthStatus.ViewModel.Model;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.ConfigureServices();
    })
    .ConfigureLogging((hostingContext,logging) =>
    {
        logging.ClearProviders();
        logging.AddFile(hostingContext.Configuration.GetSection("Logging"));
        logging.AddConsole();
    })
    .Build();

var logger = host.Services.GetRequiredService<ILoggerFactory>().CreateLogger<Program>();

var mainVm = host.Services.GetRequiredService<MainViewModel>();
mainVm.ModelFilePath = @"j:\temp\CatalogServiceModel.json";
await mainVm.Populate();

foreach (var service in mainVm.Children)
{
  //  Console.WriteLine($"service:");
  //  Console.WriteLine($"name: {service.Model.Name}");
  //  Console.WriteLine($"environments:");
    foreach (var environment in service.Children)
    {
       // environment.ExecuteProbe.Execute(null);
       // Console.WriteLine($"\tname: {environment.Model.Name}");
     //   Console.WriteLine($"\tinstances:");
        foreach (var instance in environment.Children)
        {
            var title = $" {service.Model.Name} {environment.Model.Name} {instance.Model.Name}";
            instance.PropertyChanged += (obj, prm) =>
            {
                 logger.LogInformation($"\t\t\t{title}: {prm.PropertyName} changed:{instance.Response}"); 
            };
           // instance.ExecuteProbe.Execute(null);
         //   Console.WriteLine($"\t\t\tname: {instance.Model.Name}");
          //  Console.WriteLine($"\t\t\turl: {instance.Model.Url}");
        }
    }

    service.ExecuteProbe.Execute(null);

    var r = Console.ReadLine();
}