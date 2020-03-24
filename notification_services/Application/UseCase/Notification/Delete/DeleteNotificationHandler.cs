using System.Threading;
using System.Threading.Tasks;
using notification_services.Presistences;
using notification_services.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace notification_services.Application.UseCase.Notification.Delete
{
        public class DeleteNotificationHandler : IRequestHandler<DeleteNotification, DeleteNotificationDto>
    {
        private readonly notification_context _context;

        public DeleteNotificationHandler(notification_context context)
        {
            _context = context;
        }
        public async Task<DeleteNotificationDto> Handle(DeleteNotification request, CancellationToken cancellationToken)
        {
            var delete = await _context.Notification.FindAsync(request.Id);

            if (delete == null)
            {
                return new DeleteNotificationDto
                {
                    Success = false,
                    Message = "Not Found"
                };
            }

            else
            { 
                _context.Notification.Remove(delete);
                await _context.SaveChangesAsync(cancellationToken);

                return new DeleteNotificationDto
                {
                    Success = true,
                    Message = "Successfully Delete Notification"
                };

            }
           
        }
    }
    
}