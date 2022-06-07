using Microsoft.EntityFrameworkCore;
using SolutionFramework.Core.Abstractions;
using SolutionFramework.EFcore.Model;
using SolutionFramework.EFcore.Repository;

namespace SolutionFramework.EFcore.Operations
{
    public abstract class OperationBase<TKey, TUserKey, TEntity> : RepositoryBase<TKey, TUserKey>, IOperationBase<TKey, TUserKey, TEntity> where TEntity : class, IEntityBase<TKey, TUserKey>
    {
        /// <summary>
        /// Lista de propiedades que serán descartadas en la actualizacionn de la entidad
        /// </summary>
        private readonly List<string> blackList = new List<string>() {
            nameof(IEntityBase<TKey, TUserKey>.Id),
            nameof(IEntityBase<TKey, TUserKey>.DateCreated),
            nameof(IEntityBase<TKey, TUserKey>.IdUserCreator)
        };

        /// <summary>
        /// Proporciona información de autenticación del usuarios durante la solicitud
        /// </summary>
        protected readonly IAuthenticatedUser<TUserKey> _authenticatedUser;
        /// <summary>
        /// inicializa una nueva instancia de la clase Operation 
        /// </summary>
        /// <param name="autenticatedUser">Información de autenticacion del usuario por solicitud</param>
        /// <param name="context">Representa la sesion con la BD</param>
        protected OperationBase(IAuthenticatedUser<TUserKey> autenticatedUser, DbContext context) : base(context)
        {
            _authenticatedUser = autenticatedUser;
        }


        /// <summary>
        /// Metodo que crea un registro en la BD
        /// </summary>
        /// <param name="entity">Entidad a crear</param>
        /// <param name="cancellation">Propaga la notificacion para la operación de cancelación</param>
        /// <returns>Representa una operación asincrona que puede retornar un valor</returns>
        public virtual async Task<TKey> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            entity.IdUserCreator = _authenticatedUser.Id;
            entity.DateCreated = DateTime.UtcNow;
            entity = await base.CreateAsync(entity, cancellationToken);
            return entity.Id;
        }

        /// <summary>
        /// Base cuando se desea hacer uso de las implementaciones solo del padre de clase, y  this cuando se desea hacer uso de las implementaciones del padre y del hijo 
        /// </summary>
        /// <param name="id">Id del registro a actualizar</param>
        /// <param name="cancellation">Propaga la notificacion para la operación de cancelación</param>
        /// <returns>Representa una operación asincrona que puede retornar un valor</returns>
        public virtual Task<bool> DeleteAsync(TKey id, CancellationToken cancellationToken = default)
        {
            //Accediendo al Metodo DeleteAsync del Padre de la clase (RepositopryBase)
            return base.DeleteAsync<TEntity>(x => x.Id.Equals(id), cancellationToken);
        }

        /// <summary>
        /// Metodo que actualiza un registro en la BD
        /// </summary>
        /// <param name="id">Id del registro a actualizar</param>
        /// <param name="entity">Entidad que contiene la información a actualizar</param>
        /// <param name="cancellation">Propaga la notificacion para la operación de cancelación</param>
        /// <returns>Representa una operación asincrona que puede retornar un valor</returns>
        public virtual async Task<bool> UpdateAsync(TKey id, TEntity entity, CancellationToken cancellationToken = default)
        {

            var record = await base.GetEntity<TEntity>().FindAsync(id);

            if (record != null)
            {
                var properties = typeof(TEntity).GetProperties().Where(x => !blackList.Contains(x.Name)).ToList();

                foreach (var property in properties)
                {
                    var values = entity.GetType().GetProperty(property.Name).GetValue(entity);
                    if (values != null)
                        record.GetType().GetProperty(property.Name).SetValue(record, values, null);
                }
                return await base.UpdateAsync(record, cancellationToken);
            }
            return false;

        }
    }
}
