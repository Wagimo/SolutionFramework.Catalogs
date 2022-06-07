using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SolutionFramework.EFcore.Model;
using SolutionFramework.EFcore.Options;
using System.Security.Principal;

namespace SolutionFramework.EFcore.Middleweare
{
    public class IdentityServices<TUserKey> : IIdentityServices<TUserKey>
    {
        /// <summary>
        /// Define la funcionalidad básica del objeto identity
        /// </summary>
        private readonly IIdentity _identity;
        /// <summary>
        /// Opciones de configuración para SolutionFramework.EFCore
        /// </summary>
        private readonly EFCoreOptions _efCoreOptions;
        /// <summary>
        /// Proporciona información del usaurio autenticado dirante la petición
        /// </summary>
        private readonly IAuthenticatedUser<TUserKey> _autenticatedUser;
        /// <summary>
        /// IHttpContextAccessor Contexto de la peticion
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// Inicializa una nueva instancia de la clase IdentityServices
        /// </summary>
        /// <param name="option">Opciones de configuración para SolutionFramework.EFCore</param>
        /// <param name="autenticatedUser">Información del usuario autenticado durante la solicitud</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor - Contexto de la peticion</param>
        public IdentityServices(IOptions<EFCoreOptions> option, IAuthenticatedUser<TUserKey> autenticatedUser, IHttpContextAccessor httpContextAccessor)
        {
            _efCoreOptions = option.Value;
            _httpContextAccessor = httpContextAccessor;
            _identity = httpContextAccessor.HttpContext.User.Identity;
            _autenticatedUser = autenticatedUser;

        }
        /// <summary>
        /// Metodo que asigna los valores de identidad al objeto IAuthenticatedUser<TUserKey>
        /// </summary>
        /// <returns>Retorna un IAuthenticatedUser<TUserKey> con información de autenticación.</returns>
        public IAuthenticatedUser<TUserKey> BuildAuthenticatedUser()
        {
            _autenticatedUser.Authenticated = _identity.IsAuthenticated;
            if (_autenticatedUser.Authenticated)
            {
                _autenticatedUser.Name = GetUser();
                _autenticatedUser.Email = GetUserEmail();
                _autenticatedUser.Id = GetIdUser();
                _autenticatedUser.IsApplication = IsApplication();
            }
            return _autenticatedUser;
        }

        /// <summary>
        /// Metodo que obtiene el nombre de usuario del usuario autenticado basado en la configuración asignada
        /// </summary>
        /// <returns>Retorna el UserName del usuario autenticado</returns>
        private string GetUser() => GetClaimValue(_efCoreOptions.ClaimsIdentity.User);
        /// <summary>
        /// Metodo que obtiene el Correo del usuario autenticado badaso en la configuración asignada
        /// </summary>
        /// <returns>Retorna el Email del usaurio autenticado</returns>
        private string GetUserEmail() => GetClaimValue(_efCoreOptions.ClaimsIdentity.Email);
        /// <summary>
        /// Metodo que devuelve el Id del usuario autenticado basado en la configuración asignada
        /// </summary>
        /// <returns>Retorna el ID del usuario autenticado</returns>
        private TUserKey GetIdUser()
        {

            var id = _httpContextAccessor.HttpContext.User.FindFirst(_efCoreOptions.ClaimsIdentity.IdUser);
            if (id != null)
            {
                return (TUserKey)Convert.ChangeType(id.Value, typeof(TUserKey));
            }

            return default;
        }
        /// <summary>
        /// Metodo que valida si el Identity es una aplicación basado en el claim rol.
        /// </summary>
        /// <returns>Retorna True si es una aplicación, false en otros casos</returns>
        private bool IsApplication()
        {
            var role = _httpContextAccessor.HttpContext.User.FindFirst(_efCoreOptions.ClaimsIdentity.Role);
            return role == null;
        }
        /// <summary>
        /// Metodo que consulta la información del Claim
        /// </summary>
        /// <param name="claimType">Claim desde el cual será obtenido el valor</param>
        /// <returns>Retorna el valor del claim</returns>
        private string GetClaimValue(string claimType)
        {
            var user = _httpContextAccessor.HttpContext.User.FindFirst(claimType);
            if (user != null)
                return user.Value;
            return default;
        }
    }
}

