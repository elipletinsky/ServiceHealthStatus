using System;
using System.Collections;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ServiceHealthStatus.ViewModel;
using ServiceHealthStatus.ViewModel.Model;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.ConfigureServices();
    })
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
    })
    .Build();

var mainVm = host.Services.GetRequiredService<MainViewModel>();
mainVm.ModelFilePath = @"j:\temp\CatalogServiceModel.json";
await mainVm.Populate();

//var services = await Service.Load(@"j:\temp\CatalogServiceModel.json");

foreach (var service in mainVm.Children)
{
    service.ExecuteProb.Execute(null);
    Console.WriteLine($"service:");
    Console.WriteLine($"name: {service.Model.Name}");
    Console.WriteLine($"environments:");
    foreach (var environment in service.Children)
    {
        environment.ExecuteProb.Execute(null);
        Console.WriteLine($"\tname: {environment.Model.Name}");
        Console.WriteLine($"\tinstances:");
        foreach (var instance in environment.Children)
        {
            instance.ExecuteProb.Execute(null);
            Console.WriteLine($"\t\t\tname: {instance.Model.Name}");
            Console.WriteLine($"\t\t\turl: {instance.Model.Url}");
        }
    }
}