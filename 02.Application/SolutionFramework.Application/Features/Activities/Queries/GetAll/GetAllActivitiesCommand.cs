using MediatR;
using SolutionFramework.Application.ViewModels.Activities;

namespace SolutionFramework.Application.Features.Activities.Queries.GetAll
{
    public class GetAllActivitiesCommand : IRequest<List<ActivityVm>>
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
    }
}
