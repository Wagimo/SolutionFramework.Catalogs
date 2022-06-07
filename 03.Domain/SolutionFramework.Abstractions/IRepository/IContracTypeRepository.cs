using SolutionFramework.EFcore.Operations;
using SolutionFramework.Entities.Entities;

namespace SolutionFramework.Abstractions.IRepository
{
    public interface IContracTypeRepository : IOperationBase<Guid, string, ContractType>
    {
    }
}
