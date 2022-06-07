using FluentValidation;
using MediatR;
using ValidationException = SolutionFramework.Exceptions.ValidationException;

namespace SolutionFramework.Application.Behaviors
{
    /// <summary>
    /// Ejecuta logica de validación dentro del pipeline antes de que lleguen las solicitudes
    /// al Handler. 
    /// </summary>
    /// <typeparam name="TRequest">Solicitud enviada por el cliente</typeparam>
    /// <typeparam name="TResponse">Respuesta condicional dependiendo de si hubo o no una excepción.</typeparam>
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResult = await Task
                                            .WhenAll(_validators
                                                .Select(x => x
                                                   .ValidateAsync(context, cancellationToken)));
                var result = validationResult.SelectMany(r => r.Errors).Where(x => x != null).ToList();
                if (result.Any())
                    throw new ValidationException(result);

            }

            return await next();
        }
    }
}
