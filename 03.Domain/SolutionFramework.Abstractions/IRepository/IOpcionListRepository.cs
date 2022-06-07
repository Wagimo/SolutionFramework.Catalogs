using SolutionFramework.EFcore.Operations;
using SolutionFramework.Entities.Entities;

namespace SolutionFramework.Abstractions.IRepository
{
    public interface IOpcionListRepository : IOperationBase<Guid, string, OptionList>
    {
    }
}
