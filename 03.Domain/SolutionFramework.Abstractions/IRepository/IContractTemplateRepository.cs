using SolutionFramework.EFcore.Operations;
using SolutionFramework.Entities.Entities;

namespace SolutionFramework.Abstractions.IRepository
{
    public interface IContractTemplateRepository : IOperationBase<Guid, string, ContractTemplate>
    {
    }
}
