using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SolutionFramework.SqlServer
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SqlServerContext>(opt =>
            {
                if (!opt.IsConfigured)
                {
                    opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                        SqlOptions =>
                        {
                            SqlOptions.MigrationsAssembly("SolutionFrameWork.API");
                            SqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(10), errorNumbersToAdd: null);
                        }
                    );
                }
            });


            //services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
            //services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}
