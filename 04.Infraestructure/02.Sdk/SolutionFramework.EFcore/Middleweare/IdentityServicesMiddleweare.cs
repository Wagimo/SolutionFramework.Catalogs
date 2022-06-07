using Microsoft.AspNetCore.Http;

namespace SolutionFramework.EFcore.Middleweare
{
    public class IdentityServicesMiddleweare<TuserKey>
    {
        /// <summary>
        /// Funcion que puede procesar una peticion HTTP
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// inicializa una nueva instancia de IdentityServicesMiddleweare
        /// Servicios de Singleton y Trasient se registran en el constructor
        /// </summary>
        /// <param name="next"></param>
        public IdentityServicesMiddleweare(RequestDelegate next)
        {
            _next = next;
        }
        /// <summary>
        /// Método invocado en el Flujo de la Solicitus
        /// </summary>
        /// <param name="context">Contexto HTTP para la solicitud actual</param>
        /// <param name="identityServices">El servicio de identidad IIdentityServices para la solicitud actual, los cuales estan registrados a nivel de Scoped</param>
        /// <returns>Retorna una tarea que representa la ejecucion del Middleweare</returns>
        public async Task InvokeAsync(HttpContext context, IIdentityServices<TuserKey> identityServices)
        {
            identityServices.BuildAuthenticatedUser();
            await _next(context);
        }
    }
}
