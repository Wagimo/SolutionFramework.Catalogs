using SolutionFramework.EFcore.Operations;
using SolutionFramework.Entities.Entities;

namespace SolutionFramework.Abstractions.IRepository
{
    public interface IProviderRepository : IOperationBase<Guid, string, Provider>
    {
    }
}
