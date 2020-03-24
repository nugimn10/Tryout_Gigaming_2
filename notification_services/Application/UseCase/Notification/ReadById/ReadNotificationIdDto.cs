using notification_services.Domain.Entities;
using notification_services.Application.UseCase;

namespace notification_services.Application.UseCase.Notification.ReadById
{
    public class ReadNotificationIdDto : BaseDto
    {
        public NotificationDTO Data { get; set; }
    }
}