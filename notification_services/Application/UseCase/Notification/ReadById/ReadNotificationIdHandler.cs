using System.Threading;
using System.Threading.Tasks;
using notification_services.Presistences;
using notification_services.Domain.Entities;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Collections.Generic;

namespace notification_services.Application.UseCase.Notification.ReadById
{
    public class ReadNotificationIdhandler : IRequestHandler<ReadNotificationId, ReadNotificationIdDto>
    {
        private readonly notification_context _context;

        public ReadNotificationIdhandler(notification_context context)
        {
            _context = context;
        }
        public async Task<ReadNotificationIdDto> Handle(ReadNotificationId request, CancellationToken cancellationToken)
        {

            var resultNot = await _context.Notification.FirstOrDefaultAsync(e => e.id == request.Id);
            var resultLog = await _context.Notification_Logs.Where(n => n.notification_id == request.Id).ToListAsync();
           
            var ListLog = new List<NotificationLogData>();

            foreach(var n in resultLog)
            {
                ListLog.Add(new NotificationLogData(){
                    
                    Notification_id = n.notification_id,
                    From = n.from,
                    Read_at = n.read_at,
                    Target = n.target
                });
            }

            return new ReadNotificationIdDto()
            {
                Message = "Message successfully retrieved",
                Success = true,
                Data = new NotificationDTO()
                {
                    Notifications = new NotificationData()
                    {
                    Id = resultNot.id,
                    Title = resultNot.title,
                    Message = resultNot.message
                    },
                    Notification_logs = ListLog
                }
            };

        }
    }
}