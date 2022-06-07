namespace SolutionFramework.Application.ViewModels.Activities
{
    public class ActivityVm
    {
        public Guid Id { get; set; }
        public string IdUserCreator { get; set; }
        public bool State { get; set; }
        public DateTime DateCreated { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
