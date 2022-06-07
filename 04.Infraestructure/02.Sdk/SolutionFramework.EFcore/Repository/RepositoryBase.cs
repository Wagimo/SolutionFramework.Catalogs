using Microsoft.EntityFrameworkCore;
using SolutionFramework.Core.Abstractions;
using System.Linq.Expressions;

namespace SolutionFramework.EFcore.Repository
{
    /// <summary>
    /// Clase que implemeta las los metodos mas utilizados para la concurrencia a la BD  definidos en la interface
    /// </summary>
    public abstract class RepositoryBase<TKey, TUserKey> : IRepositoryBase<TKey, TUserKey>
    {
        /// <summary>
        ///  A DbContext instance represents a session with the database and can be used to
        //     query and save instances of your entities. DbContext is a combination of the
        //     Unit Of Work and Repository patterns.
        /// </summary>
        protected readonly DbContext _dbContext;
        /// <summary>
        /// Crea una nueva instancia del repositorio
        /// <param name="context">represents a session with the database and can be used to query and save instances of your entities</param>
        /// <exception cref="ArgumentNullException">Si el context es nulo</exception>
        /// </summary>
        protected RepositoryBase(DbContext context)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }
        /// <summary>
        /// Transforma el DBContext en un contexto tipo generico. En los microservicios pueden existir multiples contextos y no es posible saber cual 
        /// va a llegar a la clase. Lo que si se sabe es que es un DBContext (por eso la restricción). Sabiendo eso, es posible transformar el DbContext en TContexto que es el 
        /// contexto utilizado por la APP.
        /// </summary>
        /// <typeparam name="TContext">tipo de contexto a retornar</typeparam>
        /// <returns>Retorna el contexto de la BD</returns>
        public TContext GetContext<TContext>() where TContext : DbContext => (TContext)_dbContext;

        /// <summary>
        /// Obtiene un DbSet el cual se encarga de realizar las consultas, actualizaciones e insersiones de las instancias de la TEntity en la BD
        /// </summary>
        /// <typeparam name="TEntity">Tipo de entidad que será retornado</typeparam>
        /// <returns>La entidad suministrada en el metodo generico con los datos asociados a propiedades estándar de las entidades</returns>
        public DbSet<TEntity> GetEntity<TEntity>() where TEntity : class, IEntityBase<TKey, TUserKey> => _dbContext.Set<TEntity>();

        /// <summary>
        /// Metodo que crea una entidad  en la Bd
        /// </summary>
        /// <typeparam name="TEntity">Tipo de entidad a crear</typeparam>
        /// <param name="entity">Entidad creada</param>
        /// <param name="cancellation">Propaga la notificacion para la operación de cancelación</param>
        /// <returns>Representa una operación asincrona que puede retornar un valor</returns>
        public Task<TEntity> CreateAsync<TEntity>(TEntity entity, CancellationToken cancellation = default) where TEntity : class, IEntityBase<TKey, TUserKey>
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return ProcessCreateAsync(entity, cancellation);
        }
        /// <summary>
        /// Metodo que crea una entidad  en la Bd
        /// </summary>
        /// <typeparam name="TEntity">Tipo de entidad a crear</typeparam>
        /// <param name="entity">Entidad creada</param>
        /// <param name="cancellation">Propaga la notificacion para la operación de cancelación</param>
        /// <returns>Representa una operación asincrona que puede retornar un valor</returns>
        private async Task<TEntity> ProcessCreateAsync<TEntity>(TEntity entity, CancellationToken cancellation) where TEntity : class, IEntityBase<TKey, TUserKey>
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync(cancellation);
            return entity;
        }

        /// <summary>
        /// Metodo que Actualiza una entidad  en la Bd
        /// </summary>
        /// <typeparam name="TEntity">Tipo de entidad a actualizar</typeparam>
        /// <param name="entity">Entidad actualizada</param>
        /// <param name="cancellation">Propaga la notificacion para la operación de cancelación</param>
        /// <returns>Representa una operación asincrona que puede retornar un valor</returns>
        public Task<bool> UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellation = default) where TEntity : class, IEntityBase<TKey, TUserKey>
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return ProcessUpdateAsync(entity, cancellation);
        }
        /// <summary>
        /// Metodo que Actualiza una entidad  en la Bd
        /// </summary>
        /// <typeparam name="TEntity">Tipo de entidad a actualizar</typeparam>
        /// <param name="entity">Entidad actualizada</param>
        /// <param name="cancellation">Propaga la notificacion para la operación de cancelación</param>
        /// <returns>Representa una operación asincrona que puede retornar un valor</returns>
        private async Task<bool> ProcessUpdateAsync<TEntity>(TEntity entity, CancellationToken cancellation) where TEntity : class, IEntityBase<TKey, TUserKey>
        {
            _dbContext.Set<TEntity>().Update(entity);
            _dbContext.Entry(entity).Property(nameof(IEntityBase<TKey, TUserKey>.IdUserCreator)).IsModified = false;
            _dbContext.Entry(entity).Property(nameof(IEntityBase<TKey, TUserKey>.DateCreated)).IsModified = false;
            var success = await _dbContext.SaveChangesAsync(cancellation) > 0;
            return success;
        }
        /// <summary>
        /// Metodo que se encargara de eliminar una entidad dela BD
        /// </summary>
        /// <typeparam name="TEntity">Tipo de entidad a eliminar</typeparam>
        /// <param name="predicate">Una función para probar cada elemento para una condición.</param>
        /// <param name="cancellation">Propaga la notificacion para la operación de cancelación</param>
        /// <returns>Representa una operación asincrona que puede retornar un valor</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public Task<bool> DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellation = default) where TEntity : class, IEntityBase<TKey, TUserKey>
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return ProcessDeleteAsync(predicate, cancellation);
        }

        /// <summary>
        /// Metodo que se encargara de eliminar una entidad dela BD
        /// </summary>
        /// <typeparam name="TEntity">Tipo de entidad a eliminar</typeparam>
        /// <param name="predicate">Una función para probar cada elemento para una condición.</param>
        /// <param name="cancellation">Propaga la notificacion para la operación de cancelación</param>
        /// <returns>Representa una operación asincrona que puede retornar un valor</returns>
        /// <exception cref="ArgumentNullException"></exception>
        private async Task<bool> ProcessDeleteAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellation) where TEntity : class, IEntityBase<TKey, TUserKey>
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(TEntity));

            var entidad = await _dbContext.Set<TEntity>().Where(predicate).FirstOrDefaultAsync();

            if (entidad != null)
            {
                _dbContext.Set<TEntity>().Remove(entidad);
                return await _dbContext.SaveChangesAsync(cancellation) > 0;
            }

            return false;
        }

        /// <summary>
        /// Metodo que crea en la base de datos una lista de entidades.
        /// </summary>
        /// <typeparam name="TEntity">Tipo de entidad a crear</typeparam>
        /// <param name="entities">Lista de Entidades a crear</param>
        /// <param name="cancellation">Propaga la notificacion para la operación de cancelación</param>
        /// <returns>Representa una operación asincrona que puede retornar un valor</returns>
        public async Task<List<TEntity>> CreateRangeAsync<TEntity>(List<TEntity> entities, CancellationToken cancellation = default) where TEntity : class, IEntityBase<TKey, TUserKey>
        {
            if (!entities.Any())
                return entities;

            return await ProcessCreateRangeAsync(entities, cancellation);
        }

        /// <summary>
        /// Metodo que crea en la base de datos una lista de entidades.
        /// </summary>
        /// <typeparam name="TEntity">Tipo de entidad a crear</typeparam>
        /// <param name="entities">Lista de Entidades a crear</param>
        /// <param name="cancellation">Propaga la notificacion para la operación de cancelación</param>
        /// <returns>Representa una operación asincrona que puede retornar un valor</returns>
        private async Task<List<TEntity>> ProcessCreateRangeAsync<TEntity>(List<TEntity> entities, CancellationToken cancellation) where TEntity : class, IEntityBase<TKey, TUserKey>
        {
            await _dbContext.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync(cancellation);
            return entities;
        }

        /// <summary>
        /// Metodo que actualiza en la base de datos una lista de entidades.
        /// </summary>
        /// <typeparam name="TEntity">Tipo de entidad a actualizar</typeparam>
        /// <param name="entities">Lista de Entidades a actualiar</param>
        /// <param name="cancellation">Propaga la notificacion para la operación de cancelación</param>
        /// <returns>Representa una operación asincrona que puede retornar un valor</returns>
        public async Task<bool> UpdateRangeAsync<TEntity>(List<TEntity> entities, CancellationToken cancellation = default) where TEntity : class, IEntityBase<TKey, TUserKey>
        {
            if (!entities.Any())
                return false;

            _dbContext.UpdateRange(entities);
            return await _dbContext.SaveChangesAsync(cancellation) == entities.Count;
        }

        /// <summary>
        /// Metodo que elimina en la base de datos una lista de entidades.
        /// </summary>
        /// <typeparam name="TEntity">Tipo de entidad a eliminar</typeparam>
        /// <param name="entities">Lista de Entidades a eliminar</param>
        /// <param name="cancellation">Propaga la notificacion para la operación de cancelación</param>
        /// <returns>Representa una operación asincrona que puede retornar un valor</returns>
        public async Task<bool> DeleteRangeAsync<TEntity>(List<TEntity> entities, CancellationToken cancellation = default) where TEntity : class, IEntityBase<TKey, TUserKey>
        {
            if (!entities.Any())
                return false;

            _dbContext.RemoveRange(entities);
            return await _dbContext.SaveChangesAsync(cancellation) == entities.Count;
        }

        /// <summary>
        /// Metodo que permite cambiar el estado de un registro en la BD
        /// </summary>
        /// <typeparam name="TEntity">Entidad a actualziar</typeparam>
        /// <param name="id">Identificador de la entidad a actualizar</param>
        /// <param name="state">Valor que sera asignado si la entidad existe</param>
        /// <param name="cancellation">Propaga la notificacion para la operación de cancelación</param>
        /// <returns>Representa una operación asincrona que puede retornar un valor</returns>
        public async Task<bool> ChangeStateAsync<TEntity>(TKey id, bool state, CancellationToken cancellation = default) where TEntity : class, IEntityBase<TKey, TUserKey>
        {
            var entity = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (entity != null)
            {
                entity.State = state;
                return await _dbContext.SaveChangesAsync(cancellation) > 0;
            }

            return false;
        }

        /// <summary>
        /// Metodo que permite multiples procesos en la BD en una sola transacción
        /// </summary>
        /// <typeparam name="TResult">Tipo de dato que retorna si el proceso es satisfactorio</typeparam>
        /// <param name="process">Proceso a ejecutar en el flujo de transacciones</param>
        /// <param name="isolation">especifica el comportamiento de bloqueo de transacciones para la conexión</param>
        /// <param name="cancellation">Propaga la notificacion para la operación de cancelación</param>
        /// <returns>Representa una operación asincrona que puede retornar un valor</returns>
        public async Task<TResult> TransactionAsync<TResult>(Func<DbContext, Task<TResult>> process, System.Data.IsolationLevel isolation = System.Data.IsolationLevel.ReadUncommitted, CancellationToken cancellationToken = default)
        {
            var strategy = _dbContext.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync(async (cancellation) =>
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync(isolation, cancellation);
                var result = await process(_dbContext);
                await transaction.CommitAsync();
                return result;
            }, cancellationToken);
        }

    }
}
