using System.ComponentModel.DataAnnotations;

namespace SolutionFramework.EFcore.Options
{
    /// <summary>
    /// Claims disponibles para obtener lainformación del usuario desde el Token
    /// </summary>
    public class ClaimsOptions
    {
        /// <summary>
        /// Obtiene o Establede el claim con información de nombre del usuario
        /// </summary>
        [Required]
        public string User { get; set; } = string.Empty;
        /// <summary>
        /// Obtiene o establece el claim con información del Id del Usuario
        /// </summary>
        [Required]
        public string IdUser { get; set; } = string.Empty;
        /// <summary>
        /// Obtiene o establece el claim con infrmación del coreo electronico del usuario
        /// </summary>
        [Required]
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// Obtiene o establece el claim con información del rol o roles del usuario
        /// </summary>
        [Required]
        public string Role { get; set; } = string.Empty;
    }
}
