
using ServiceHealthStatus.ViewModel;
using System;
using System.Collections;


var services = await Service.Load(@"j:\temp\CatalogServiceModel.json");

foreach (var service in services)
{
    Console.WriteLine($"sevice:");
    Console.WriteLine($"name: {service.Name}");
    Console.WriteLine($"environments:");
    foreach (var environment in service.Environments)
    {
        Console.WriteLine($"\tname: {environment.Name}");
        Console.WriteLine($"\tinstances:");
        foreach (var instance in environment.Instances)
        {
            Console.WriteLine($"\t\t\tname: {instance.Name}");
            Console.WriteLine($"\t\t\turl: {instance.Url}");
        }
    }
}