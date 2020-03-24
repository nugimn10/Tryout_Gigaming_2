using System.Threading;
using System.Threading.Tasks;
using notification_services.Presistences;
using notification_services.Domain.Entities;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Collections.Generic;

namespace notification_services.Application.UseCase.Notification.ReadWithLog
{
    public class ReadNotificationIdhandler : IRequestHandler<ReadNotificationWithLog, ReadNotificationWithLogsDto>
    {
        private readonly notification_context _context;

        public ReadNotificationIdhandler(notification_context context)
        {
            _context = context;
        }
        public async Task<ReadNotificationWithLogsDto> Handle(ReadNotificationWithLog request, CancellationToken cancellationToken)
        {

            var resultData = await _context.Notification.ToListAsync();
            var resultLog = await _context.Notification_Logs.ToListAsync();
            var ListNotif = new List<NotificationDTO>();

            foreach(var n in resultData)
            {
                var logList = new List<NotificationLogData>();
                var logs = resultLog.Where(y => y.notification_id == n.id);
                foreach (var y in logs)
                { 
                    logList.Add(new NotificationLogData(){
                        
                        Notification_id = y.notification_id,
                        From = y.from,
                        Read_at = y.read_at,
                        Target = y.target
                    });

                    ListNotif.Add(new NotificationDTO()
                    {
                        Notifications = new NotificationData()
                        {
                            Id = n.id,
                            Title = n.title,
                            Message = n.message
                        },
                        Notification_logs = logList
                    });
                }
            }

            return new ReadNotificationWithLogsDto()
            {
                Message = "Successfully retrieving data",
                Success = true,
                Data = ListNotif
            };

        }
    }
}