using SolutionFramework.Abstractions.IRepository;
using SolutionFramework.EFcore.Model;
using SolutionFramework.EFcore.Operations;
using SolutionFramework.Entities.Entities;

namespace SolutionFramework.SqlServer.Repository
{
    public class CompanyRepopsitory : OperationBase<Guid, string, Company>, ICompanyRepopsitory
    {
        public CompanyRepopsitory(IAuthenticatedUser<string> autenticatedUser, SqlServerContext context) : base(autenticatedUser, context)
        {
        }
    }
}
