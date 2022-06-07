using SolutionFramework.Abstractions.IRepository;
using SolutionFramework.EFcore.Model;
using SolutionFramework.EFcore.Operations;
using SolutionFramework.Entities.Entities;

namespace SolutionFramework.SqlServer.Repository
{
    public class MenuItemRepository : OperationBase<Guid, string, MenuItem>, IMenuItemRepository
    {
        public MenuItemRepository(IAuthenticatedUser<string> autenticatedUser, SqlServerContext context) : base(autenticatedUser, context)
        {
        }
    }
}
