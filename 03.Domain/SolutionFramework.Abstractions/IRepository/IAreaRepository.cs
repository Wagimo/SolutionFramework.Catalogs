using SolutionFramework.EFcore.Operations;
using SolutionFramework.Entities.Entities;

namespace SolutionFramework.Abstractions.IRepository
{
    public interface IAreaRepository : IOperationBase<Guid, string, Area>
    {
    }
}
