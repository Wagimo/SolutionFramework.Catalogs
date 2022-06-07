using SolutionFramework.EFcore.Operations;
using SolutionFramework.Entities.Entities;

namespace SolutionFramework.Abstractions.IRepository
{
    public interface IActivityRepository : IOperationBase<Guid, string, Activity>
    {
    }
}
