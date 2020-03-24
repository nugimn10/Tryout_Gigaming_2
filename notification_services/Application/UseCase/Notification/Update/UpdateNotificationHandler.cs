using System;
using System.Threading;
using System.Threading.Tasks;
using notification_services.Presistences;
using notification_services.Application.UseCase;
using notification_services.Domain.Entities;
using MediatR;
using System.Linq;

namespace notification_services.Application.UseCase.Notification.Update
{
    public class UpdateNotificationHandler: IRequestHandler<UpdateNotification, UpdateNotificationDto>
    {
        private readonly notification_context _context;
        public UpdateNotificationHandler(notification_context context)
        {
            _context = context;
        }
        public async Task<UpdateNotificationDto> Handle(UpdateNotification request, CancellationToken cancellationToken)
        {
            var resultLog = _context.Notification_Logs.ToList();
            var query = resultLog.Where(n => n.notification_id == request.Data.Attributes.Notification_id);

            foreach (var x in request.Data.Attributes.Target)
            {
                var data = query.First(j => j.target == x.Id).id;
                var dataContext = await _context.Notification_Logs.FindAsync(data);
                dataContext.read_at = request.Data.Attributes.Read_at;
                await _context.SaveChangesAsync();
            }

            await _context.SaveChangesAsync();

            return new UpdateNotificationDto()
            {
                Message = "Data successfully updated",
                Success = true,
                
            };
        }
    }
}