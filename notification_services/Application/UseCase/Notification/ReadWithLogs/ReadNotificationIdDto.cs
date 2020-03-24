using notification_services.Domain.Entities;
using notification_services.Application.UseCase;
using System.Collections.Generic;

namespace notification_services.Application.UseCase.Notification.ReadWithLog
{
    public class ReadNotificationWithLogsDto : BaseDto
    {
        public List<NotificationDTO> Data { get; set; }
    }
}