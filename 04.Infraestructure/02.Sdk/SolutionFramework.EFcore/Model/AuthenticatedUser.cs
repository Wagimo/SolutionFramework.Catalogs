namespace SolutionFramework.EFcore.Model
{
    public class AuthenticatedUser<TKeyUser> : IAuthenticatedUser<TKeyUser>
    {
        public TKeyUser Id { get; set; }
        public bool Authenticated { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsApplication { get; set; }
    }
}
