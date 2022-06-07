using SolutionFramework.Core.Abstractions;

namespace SolutionFramework.EFcore.Operations
{
    public interface IUpdateOperation<TKey, TUserKey, TEntity> where TEntity : class, IEntityBase<TKey, TUserKey>
    {
        Task<bool> UpdateAsync(TKey id, TEntity entity, CancellationToken cancellationToken = default);
    }
}
