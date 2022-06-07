namespace SolutionFramework.EFcore.Model
{
    public interface IAuthenticatedUser<TKeyUser>
    {
        TKeyUser Id { get; set; }
        bool Authenticated { get; set; }
        string Name { get; set; }
        string Email { get; set; }
        bool IsApplication { get; set; }
    }
}
