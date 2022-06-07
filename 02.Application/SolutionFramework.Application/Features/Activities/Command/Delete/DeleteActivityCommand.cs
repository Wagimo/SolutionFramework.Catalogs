using MediatR;

namespace SolutionFramework.Application.Features.Activities.Command.Delete
{
    public class DeleteActivityCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
