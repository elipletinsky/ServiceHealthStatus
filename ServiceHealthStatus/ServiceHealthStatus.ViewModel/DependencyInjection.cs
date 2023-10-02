using Microsoft.Extensions.DependencyInjection;

namespace ServiceHealthStatus.ViewModel
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddHttpClient<IStatusProbeService, StatusProbeService>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<ServiceViewModel>();
            services.AddTransient<EnvironmentViewModel>();
            services.AddTransient<ServiceInstanceViewModel>();
            return services;
        }
    }
}
