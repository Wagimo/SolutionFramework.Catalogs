using MediatR;
using SolutionFramework.Application.ViewModels.Activities;

namespace SolutionFramework.Application.Features.Activities.Queries.GetById
{
    public class GetByIdActivityCommand : IRequest<ActivityVm>
    {
        public Guid Id { get; set; }
    }
}
