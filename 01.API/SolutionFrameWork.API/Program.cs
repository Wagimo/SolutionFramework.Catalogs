using SolutionFramework.Application;
using SolutionFramework.EFcore.Extensions;
using SolutionFramework.LoggerProvider;
using SolutionFramework.SqlServer;
using SolutionFrameWork.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//Registrando los servicios de la capa de infraestructura (DdContext)
builder.Services.AddInfrastructureServices(builder.Configuration);
//Registrando los valores para las opciones de EFCore Options
builder.Services.AddEfCore(builder.Configuration)
    .AddIdentityServices<string>()
    .AddRepositories<Guid, string, SqlServerContext>();
//Registrando los servicios de la capa de aplication. (Contracts)
builder.Services.AddApplicationServices();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var version = "v1";
    options.SwaggerDoc(version, new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Title = $"SolutionsFramework - {version}",
        Description = "Arquitectura Base para la Construcción de Microservicios",
        Version = version,
        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
        {
            Name = "SOLVER DE COLOMBIA S.A.S",
            Email = "contacto@solvercol.com.co",
            Url = new Uri(@"Https://solvercol.com.co")
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    );
});



var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
//1) Middleware que Valida si el usuario esta  Autenticacion
//app.UseAuthentication();
//2) Middleware que valida si el usuario esta autorizado
app.UseAuthorization();
//3) Middleware que registra la información de Usuario Autenticado en la Clase IAuthenticatedUser para tenerla disponible como un Singleton
app.UseIdentityServices<string>();


if (!app.Environment.IsDevelopment())
{
    //Configurando Papertrail LogSystem
    var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
    if (loggerFactory != null)
    {
        loggerFactory.AddSyslog(
                builder.Configuration.GetValue<string>("Papertrail:host"),
                builder.Configuration.GetValue<int>("Papertrail:port")
                );
    }
}


// Configure OpenAPI.
app.UseSwagger();

app.UseSwaggerUI(x =>
{
    x.SwaggerEndpoint("/swagger/v1/swagger.json", "SolutionsFramework");
});

app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
