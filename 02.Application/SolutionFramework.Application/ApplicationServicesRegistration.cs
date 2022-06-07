using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SolutionFramework.Application.Behaviors;
using System.Reflection;

namespace SolutionFramework.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());


            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledBehaviorException<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
    }
}
