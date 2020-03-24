using System.Threading;
using System.Threading.Tasks;
using payment_services.Presistences;
using payment_services.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace payment_services.Application.UseCase.payment.Delete
{
        public class DeleteNotificationHandler : IRequestHandler<Deletepayment, DeletePaymentDto>
    {
        private readonly payment_context _context;

        public DeleteNotificationHandler(payment_context context)
        {
            _context = context;
        }
        public async Task<DeletePaymentDto> Handle(Deletepayment request, CancellationToken cancellationToken)
        {
            var delete = await _context.payments.FindAsync(request.Id);

            if (delete == null)
            {
                return new DeletePaymentDto
                {
                    Success = false,
                    Message = "Not Found"
                };
            }

            else
            { 
                _context.payments.Remove(delete);
                await _context.SaveChangesAsync(cancellationToken);

                return new DeletePaymentDto
                {
                    Success = true,
                    Message = "Successfully Delete Notification"
                };

            }
           
        }
    }
    
}