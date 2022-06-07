using Microsoft.EntityFrameworkCore;
using SolutionFramework.Core.Abstractions;
using System.Linq.Expressions;

namespace SolutionFramework.EFcore.Repository
{
    /// <summary>
    /// Expone los metodos base para llevar a cabo las operaciones con la BD
    /// </summary>
    public interface IRepositoryBase<TKey, TUserKey>
    {

        TContext GetContext<TContext>() where TContext : DbContext;

        /// <summary>
        /// Obtiene un DbSet el cual se encarga de realizar las consultas, actualizaciones e insersiones de las instancias de la TEntity en la BD
        /// </summary>
        /// <typeparam name="TEntity">Tipo de entidad que será retornado</typeparam>
        /// <returns>La entidad suministrada en el metodo generico con los datos asociados a propiedades estándar de las entidades</returns>
        DbSet<TEntity> GetEntity<TEntity>() where TEntity : class, IEntityBase<TKey, TUserKey>;

        /// <summary>
        /// Metodo que crea una entidad  en la Bd
        /// </summary>
        /// <typeparam name="TEntity">Tipo de entidad a crear</typeparam>
        /// <param name="entity">Entidad creada</param>
        /// <param name="cancellation">Propaga la notificacion para la operación de cancelación</param>
        /// <returns>Representa una operación asincrona que puede retornar un valor</returns>
        Task<TEntity> CreateAsync<TEntity>(TEntity entity, CancellationToken cancellation = default) where TEntity : class, IEntityBase<TKey, TUserKey>;


        /// <summary>
        /// Metodo que Actualiza una entidad  en la Bd
        /// </summary>
        /// <typeparam name="TEntity">Tipo de entidad a actualizar</typeparam>
        /// <param name="entity">Entidad actualizada</param>
        /// <param name="cancellation">Propaga la notificacion para la operación de cancelación</param>
        /// <returns>Representa una operación asincrona que puede retornar un valor</returns>
        Task<bool> UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellation = default) where TEntity : class, IEntityBase<TKey, TUserKey>;


        /// <summary>
        /// Metodo que se encargara de eliminar una entidad dela BD
        /// </summary>
        /// <typeparam name="TEntity">Tipo de entidad a eliminar</typeparam>
        /// <param name="predicate">Una función para probar cada elemento para una condición.</param>
        /// <param name="cancellation">Propaga la notificacion para la operación de cancelación</param>
        /// <returns>Representa una operación asincrona que puede retornar un valor</returns>
        /// <exception cref="ArgumentNullException"></exception>
        Task<bool> DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellation = default) where TEntity : class, IEntityBase<TKey, TUserKey>;



        /// <summary>
        /// Metodo que crea en la base de datos una lista de entidades.
        /// </summary>
        /// <typeparam name="TEntity">Tipo de entidad a crear</typeparam>
        /// <param name="entities">Lista de Entidades a crear</param>
        /// <param name="cancellation">Propaga la notificacion para la operación de cancelación</param>
        /// <returns>Representa una operación asincrona que puede retornar un valor</returns>
        Task<List<TEntity>> CreateRangeAsync<TEntity>(List<TEntity> entities, CancellationToken cancellation = default) where TEntity : class, IEntityBase<TKey, TUserKey>;


        /// <summary>
        /// Metodo que actualiza en la base de datos una lista de entidades.
        /// </summary>
        /// <typeparam name="TEntity">Tipo de entidad a actualizar</typeparam>
        /// <param name="entities">Lista de Entidades a actualiar</param>
        /// <param name="cancellation">Propaga la notificacion para la operación de cancelación</param>
        /// <returns>Representa una operación asincrona que puede retornar un valor</returns>
        Task<bool> UpdateRangeAsync<TEntity>(List<TEntity> entities, CancellationToken cancellation = default) where TEntity : class, IEntityBase<TKey, TUserKey>;

        /// <summary>
        /// Metodo que elimina en la base de datos una lista de entidades.
        /// </summary>
        /// <typeparam name="TEntity">Tipo de entidad a eliminar</typeparam>
        /// <param name="entities">Lista de Entidades a eliminar</param>
        /// <param name="cancellation">Propaga la notificacion para la operación de cancelación</param>
        /// <returns>Representa una operación asincrona que puede retornar un valor</returns>
        Task<bool> DeleteRangeAsync<TEntity>(List<TEntity> entities, CancellationToken cancellation) where TEntity : class, IEntityBase<TKey, TUserKey>;

        /// <summary>
        /// Metodo que permite cambiar el estado de un registro en la BD
        /// </summary>
        /// <typeparam name="TEntity">Entidad a actualziar</typeparam>
        /// <param name="id">Identificador de la entidad a actualizar</param>
        /// <param name="state">Valor que sera asignado si la entidad existe</param>
        /// <param name="cancellation">Propaga la notificacion para la operación de cancelación</param>
        /// <returns>Representa una operación asincrona que puede retornar un valor</returns>
        Task<bool> ChangeStateAsync<TEntity>(TKey id, bool state, CancellationToken cancellation) where TEntity : class, IEntityBase<TKey, TUserKey>;

        /// <summary>
        /// Metodo que permite multiples procesos en la BD en una sola transacción
        /// </summary>
        /// <typeparam name="TResult">Tipo de dato que retorna si el proceso es satisfactorio</typeparam>
        /// <param name="process">Proceso a ejecutar en el flujo de transacciones</param>
        /// <param name="isolation">especifica el comportamiento de bloqueo de transacciones para la conexión</param>
        /// <param name="cancellation">Propaga la notificacion para la operación de cancelación</param>
        /// <returns>Representa una operación asincrona que puede retornar un valor</returns>
        Task<TResult> TransactionAsync<TResult>(Func<DbContext, Task<TResult>> process, System.Data.IsolationLevel isolation = System.Data.IsolationLevel.ReadUncommitted, CancellationToken cancellationToken = default);

    }
}
