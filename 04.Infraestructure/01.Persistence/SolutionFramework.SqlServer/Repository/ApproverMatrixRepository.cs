using SolutionFramework.Abstractions.IRepository;
using SolutionFramework.EFcore.Model;
using SolutionFramework.EFcore.Operations;
using SolutionFramework.Entities.Entities;

namespace SolutionFramework.SqlServer.Repository
{
    public class ApproverMatrixRepository : OperationBase<Guid, string, ApproverMatrix>, IApproverMatrixRepository
    {
        public ApproverMatrixRepository(IAuthenticatedUser<string> autenticatedUser, SqlServerContext context) : base(autenticatedUser, context)
        {
        }
    }
}
