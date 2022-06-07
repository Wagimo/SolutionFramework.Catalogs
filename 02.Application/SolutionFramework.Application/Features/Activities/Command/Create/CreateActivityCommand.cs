using MediatR;
using SolutionFramework.Application.ViewModels.Activities;

namespace SolutionFramework.Application.Features.Activities.Command.Create
{
    public class CreateActivityCommand : IRequest<ActivityVm>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
