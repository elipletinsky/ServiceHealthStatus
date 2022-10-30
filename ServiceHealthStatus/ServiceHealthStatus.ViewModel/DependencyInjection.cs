using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ServiceHealthStatus.ViewModel.Model;

namespace ServiceHealthStatus.ViewModel
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<MainViewModel>();
            services.AddTransient<ServiceViewModel>();
            services.AddTransient<EnvironmentViewModel>();
            services.AddTransient<ServiceInstanceViewModel>();
            return services;
        }
    }
}
