using System.ComponentModel.DataAnnotations;

namespace SolutionFramework.EFcore.Options
{
    public class EFCoreOptions
    {
        /// <summary>
        /// Obtiene o establece información del claims relacionado con Identity
        /// </summary>
        [Required]
        public ClaimsOptions ClaimsIdentity { get; set; }
    }
}
