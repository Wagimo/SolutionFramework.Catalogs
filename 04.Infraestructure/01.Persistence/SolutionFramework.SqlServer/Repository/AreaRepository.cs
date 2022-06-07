using SolutionFramework.Abstractions.IRepository;
using SolutionFramework.EFcore.Model;
using SolutionFramework.EFcore.Operations;
using SolutionFramework.Entities.Entities;

namespace SolutionFramework.SqlServer.Repository
{
    public class AreaRepository : OperationBase<Guid, string, Area>, IAreaRepository
    {
        public AreaRepository(IAuthenticatedUser<string> autenticatedUser, SqlServerContext context) : base(autenticatedUser, context)
        {
        }
    }
}
