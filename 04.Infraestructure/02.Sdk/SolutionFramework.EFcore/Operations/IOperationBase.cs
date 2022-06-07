using SolutionFramework.Core.Abstractions;
using SolutionFramework.EFcore.Repository;

namespace SolutionFramework.EFcore.Operations
{
    public interface IOperationBase<TKey, TUserKey, TEntity> :
        ICreateOperation<TKey, TUserKey, TEntity>,
        IUpdateOperation<TKey, TUserKey, TEntity>,
        IDeleteOperation<TKey, TUserKey, TEntity>,
        IRepositoryBase<TKey, TUserKey>
        where TEntity : class, IEntityBase<TKey, TUserKey>
    {
    }
}
