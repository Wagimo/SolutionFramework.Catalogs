using MediatR;

namespace SolutionFramework.Application.Features.Activities.Command.Update
{
    public class UpdateActivityCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool State { get; set; }
    }
}
