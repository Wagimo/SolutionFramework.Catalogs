using SolutionFramework.EFcore.Operations;
using SolutionFramework.Entities.Entities;

namespace SolutionFramework.Abstractions.IRepository
{
    public interface IDocumentTypeRepository : IOperationBase<Guid, string, DocumentType>
    {
    }
}
