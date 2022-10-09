using CMS.Models;

namespace CMS.Services
{
    public interface IEmailService
    {
        Task SendEmail(EmailData emailData);
    }
}
