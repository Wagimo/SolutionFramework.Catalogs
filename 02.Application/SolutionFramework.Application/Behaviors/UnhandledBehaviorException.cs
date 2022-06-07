using MediatR;
using Microsoft.Extensions.Logging;

namespace SolutionFramework.Application.Behaviors
{
    /// <summary>
    /// Valida dentro del pipeline si existe algun error en el Handler que administra el Request con el fin
    /// de generar un LOG de Errores describiendo la excepción.
    /// </summary>
    /// <typeparam name="TRequest">Petición por parte del usuario</typeparam>
    /// <typeparam name="TResponse">Respuesta condicional dependiendo de la verificación</typeparam>
    public class UnhandledBehaviorException<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public UnhandledBehaviorException(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;
                string message = $"Ocurrió un error en el request : {requestName}.";
                _logger.LogError(message, ex);
                throw;
            }
        }
    }
}
