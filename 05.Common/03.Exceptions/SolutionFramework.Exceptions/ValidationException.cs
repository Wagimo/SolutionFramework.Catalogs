using FluentValidation.Results;

namespace SolutionFramework.Exceptions
{
    /// <summary>
    /// Excepción personalizada para lanzar controladamente un error cuando  no se cumplan las reglas
    /// de validación en cada Request.
    /// </summary>
    public class ValidationException : ApplicationException
    {
        //Estructura que permite agrupar un conjunto de Mensajes de validación por cada propiedad de la entidad, en el caso de que
        //No Se cumplan.
        public IDictionary<string, string[]> Errors { get; set; }

        public ValidationException() : base("Han ocurrido uno o varios errores de validación.")
        {
            Errors = new Dictionary<string, string[]>();
        }
        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures.GroupBy(x => x.PropertyName, e => e.ErrorMessage)
                        .ToDictionary(f => f.Key, f => f.ToArray());
        }

    }
}