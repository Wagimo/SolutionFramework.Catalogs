using SolutionFramework.Core.Abstractions;

namespace SolutionFramework.EFcore.Operations
{
    public interface ICreateOperation<TKey, TUserKey, TEntity> where TEntity : class, IEntityBase<TKey, TUserKey>
    {
        Task<TKey> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}
