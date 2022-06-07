using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SolutionFramework.Core.Abstractions;
using SolutionFramework.Core.Models.Pager;
using SolutionFramework.EFcore.Middleweare;
using SolutionFramework.EFcore.Model;
using SolutionFramework.EFcore.Options;
using SolutionFramework.EFcore.Repository;
using System.Reflection;

namespace SolutionFramework.EFcore.Extensions
{
    public static class EfCoreExtensions
    {
        public static void ConfigurationBase<TKey, TUserKey, TEntity>(this EntityTypeBuilder<TEntity> builder, bool required = false, int maxLeng = 250) where TEntity : class, IEntityBase<TKey, TUserKey>
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.IdUserCreator).IsRequired(required).HasMaxLength(maxLeng);
            builder.Property(x => x.State).IsRequired();
            builder.Property(x => x.DateCreated).IsRequired();
        }

        public static async Task<Pager<TEntity>> ToPageAsync<TEntity>(this IQueryable<TEntity> query, int currentPage, int pageSize) where TEntity : class, IEntityBase
        {
            if (currentPage < 1 || pageSize < 1)
                return default;

            var totalItems = await query.CountAsync();
            var skip = (currentPage - 1) * pageSize;
            var data = await query.Skip(skip).Take(pageSize).ToListAsync();
            return new Pager<TEntity>(totalItems, data, currentPage, pageSize);
        }

        public static void AddRepositories<TKey, TUserKey, TContext>(this IServiceCollection services) where TContext : DbContext
        {
            var assembly = typeof(TContext).GetTypeInfo().Assembly;
            //@types declaracion de variable utilizando palabra reservada, por eso la @ - La clase X tiene asignada la Interface IRepositoryBase<TKey, TUserKey>
            var @types = assembly.GetTypes().Where(x => !x.IsNested && !x.IsInterface && typeof(IRepositoryBase<TKey, TUserKey>).IsAssignableFrom(x));
            //@types contiene todas las clases que implementan la interface IRepositoryBase<TKey, TUserKey>
            foreach (var type in @types)
            {
                var interfaceName = $"I{type.Name}";
                var @interface = type.GetInterface(interfaceName, false);
                services.AddTransient(@interface, type);
            }
        }

        public static void RegisterEntityConfigurations<TContext>(this ModelBuilder builder) where TContext : DbContext
        {
            var assembly = typeof(TContext).GetTypeInfo().Assembly;
            //Identificamos las clases del tipo EntityConfiguration que implementan una interface Generica cuya definición es ugual a  IEntityTypeConfiguration
            var entityConfigurationTypes = assembly.GetTypes().Where(x =>
                                                                        x.GetInterfaces().Any(x =>
                                                                            x.GetTypeInfo().IsGenericType
                                                                            && x.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));

            //Como se requiere un EntityTypeBuilder<> para inyectarselo al metodo Configure de la instancia dinámica de la clase EntityConfiguration, nos apoyamos en el ModelBuilder y usamos el metodo generico Entity<>
            //ya que  retorna un EntityTypeBuilder<TEntity>, necesario para el paso posterior.
            var entityTypeMethod = builder.GetType().GetMethods().Single(x => x.IsGenericMethod && x.Name == "Entity" && x.ReturnType.Name == "EntityTypeBuilder`1");

            //Las clases EntityConfiguration inplementan el Metodo Configure definido en la Interface  IEntityTypeConfiguration.
            //Se debe crear la instancia de la clase EntityConfiguration, invocar el método Configure de forma dinámica y pasarle el argumento
            //EntityTypeBuilder
            foreach (var type in entityConfigurationTypes)
            {

                //Se obtiene el tipo inyectado en la interface Generica IEntityTypeConfiguration que implementa la clase. IEntityTypeConfiguration<"--- TIPOBUSCADO ---">
                var genericTypeArgument = type.GetInterfaces().Single().GenericTypeArguments.Single();

                //Al metodo Generico obtenido en el paso anterior, le inyectamos el argumento del TIPO GENERICO obtenido de la interface que implementa la clase.
                //
                var genericEntityMethod = entityTypeMethod.MakeGenericMethod(genericTypeArgument);

                //Se crea la instancia de la clase.
                var instance = Activator.CreateInstance(type);

                var entityTypeBuilder = genericEntityMethod.Invoke(builder, null);
                //Se obtiene el metodo Configure, se invoca a partir de la instancia del objeto creado anterioirmente y se le pasan los parámetros necesario (EntityTypeBuilder)
                var method = instance.GetType().GetMethod("Configure").Invoke(instance, new[] { entityTypeBuilder });
            }

        }
        /// <summary>
        /// Adiciona las opciones de configuración 
        /// </summary>
        /// <param name="services">Servicio que permite registrar y configurar la información para la clase EFCore proveniente del appSettings</param>
        /// <param name="configuration">Servicio que permite obtener la sección desde el archivo appSettings</param>
        /// <param name="section">Nombre de la sección del appSetting que será leida y mapeada a los objetos correspondientes</param>
        /// <returns>IServiceCollection con la nueva información dentro del contexto</returns>
        public static IServiceCollection AddEfCore(this IServiceCollection services, IConfiguration configuration, string section = "EFCore")
        {
            services.AddOptions();
            services.Configure<EFCoreOptions>(configuration.GetSection(section));
            return services;
        }
        /// <summary>
        /// Adiciona al Scoped los servicios de autenticación. Se utiliza cuando se estan creando metodos de extensión relacionados con IServiceCollection
        /// </summary>
        /// <typeparam name="TUserKey">Tipo de dato que identificará al usuario (id)</typeparam>
        /// <param name="services">Servicio que permite registrar y configurar la información</param>
        /// <returns>IServiceCollection con la nueva información dentro del contexto</returns>
        public static IServiceCollection AddIdentityServices<TUserKey>(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAuthenticatedUser<TUserKey>, AuthenticatedUser<TUserKey>>();
            services.AddScoped<IIdentityServices<TUserKey>, IdentityServices<TUserKey>>();

            return services;
        }
        /// <summary>
        /// Agrega un middleweare de tipo IdentityServices a la canalización de la solicitud de la aplicacion. Se utiliza cuando se crean metodos de extension
        /// asociados a la clase applicationBuilder
        /// </summary>
        /// <typeparam name="TUserKey">Tipo de dato que identificará al usuario (id)</typeparam>
        /// <param name="builder">Instancia de la clase builder</param>
        /// <returns>Instance</returns>
        public static IApplicationBuilder UseIdentityServices<TUserKey>(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<IdentityServicesMiddleweare<TUserKey>>();
        }
    }
}
