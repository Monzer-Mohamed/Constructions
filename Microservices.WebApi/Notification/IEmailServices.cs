using System.Threading.Tasks;

namespace Notification
{
    public interface IEmailServices
    {
        Task sendEmail(NotificationMessage message);

    }
}
