using notification_services.Domain.Entities;
using notification_services.Application.UseCase;
using System.Collections.Generic;

namespace notification_services.Application.UseCase.Notification.ReadBy
{
    public class ReadNotificationDto : BaseDto
    {
        public IList<NotificationData> Data { get; set; }
    }
}