using SolutionFramework.EFcore.Model;

namespace SolutionFramework.EFcore.Middleweare
{
    /// <summary>
    /// Construye el objeto IAuthenticatedUser
    /// </summary>
    /// <typeparam name="TUserKey">Typo de dato que identificará al Id del Usuario</typeparam>
    public interface IIdentityServices<TUserKey>
    {
        /// <summary>
        /// Metodo que asigna los valores respectivos al objeto de autenticación
        /// </summary>
        /// <returns>retorna un objeto IAuthenticatedUser<TUserKey> con información de autenticación</returns>
        IAuthenticatedUser<TUserKey> BuildAuthenticatedUser();
    }
}