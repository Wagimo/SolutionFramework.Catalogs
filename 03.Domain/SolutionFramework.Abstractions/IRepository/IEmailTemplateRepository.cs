using SolutionFramework.EFcore.Operations;
using SolutionFramework.Entities.Entities;

namespace SolutionFramework.Abstractions.IRepository
{
    public interface IEmailTemplateRepository : IOperationBase<Guid, string, EmailTemplate>
    {
    }
}
