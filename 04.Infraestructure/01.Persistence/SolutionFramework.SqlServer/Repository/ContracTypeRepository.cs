using SolutionFramework.Abstractions.IRepository;
using SolutionFramework.EFcore.Model;
using SolutionFramework.EFcore.Operations;
using SolutionFramework.Entities.Entities;

namespace SolutionFramework.SqlServer.Repository
{
    public class ContracTypeRepository : OperationBase<Guid, string, ContractType>, IContracTypeRepository
    {
        public ContracTypeRepository(IAuthenticatedUser<string> autenticatedUser, SqlServerContext context) : base(autenticatedUser, context)
        {
        }
    }
}
