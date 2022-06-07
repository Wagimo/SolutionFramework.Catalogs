using SolutionFramework.EmailServices.Configuration;

namespace SolutionFramework.EmailServices.IServices
{
    public interface ISendGridEmailServices
    {
        Task<bool> SendEmailAsync(Email email);
    }
}
