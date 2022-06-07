using SolutionFramework.EFcore.Operations;
using SolutionFramework.Entities.Entities;

namespace SolutionFramework.Abstractions.IRepository
{
    public interface IMenuItemRepository : IOperationBase<Guid, string, MenuItem>
    {
    }
}
