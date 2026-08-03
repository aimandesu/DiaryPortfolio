using System.Threading;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.IServices
{
    public interface IEmailSender
    {
        Task SendEmailAsync(
            string toEmail,
            string subject,
            string htmlBody,
            CancellationToken cancellationToken = default);
    }
}
