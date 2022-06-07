using SolutionFramework.Core.Abstractions;

namespace SolutionFramework.EFcore.Operations
{
    public interface IDeleteOperation<TKey, TUserKey, TEntity> where TEntity : class, IEntityBase<TKey, TUserKey>
    {
        Task<bool> DeleteAsync(TKey id, CancellationToken cancellationToken = default);
    }
}
