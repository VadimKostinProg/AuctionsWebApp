using BidMasterOnline.Application.ServiceContracts;
using BidMasterOnline.Application.Services;
using BidMasterOnline.Application.Sessions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BidMasterOnline.Application
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<ICategoriesService, CategoriesService>();
            services.AddScoped<IBidsService, BidsService>();
            services.AddScoped<IAuthService, AuthService>();
            
            services.AddScoped<SessionContext>();

            services.AddScoped<IPeriodicTaskService, PeriodicTaskService>();
            services.AddSingleton<PeriodicHostedService>();
            services.AddHostedService(provider =>
                provider.GetRequiredService<PeriodicHostedService>());

            return services;
        }
    }
}
