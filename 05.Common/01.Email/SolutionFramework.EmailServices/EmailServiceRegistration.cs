using Microsoft.Extensions.DependencyInjection;
using SolutionFramework.EmailServices.Configuration;
using SolutionFramework.EmailServices.IServices;
using SolutionFramework.EmailServices.Services;

namespace SolutionFramework.EmailServices.Registration
{
    public static class EmailServiceRegistration
    {
        public static IServiceCollection AddSenGridEmailServices(this IServiceCollection services)
        {            
            services.AddTransient<ISendGridEmailServices, SendGridEmailServices>();            
            return services;
        }
    }
}
