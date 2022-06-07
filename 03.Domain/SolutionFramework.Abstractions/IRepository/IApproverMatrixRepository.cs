using SolutionFramework.EFcore.Operations;
using SolutionFramework.Entities.Entities;

namespace SolutionFramework.Abstractions.IRepository
{
    public interface IApproverMatrixRepository : IOperationBase<Guid, string, ApproverMatrix>
    {
    }
}
