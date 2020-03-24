using System;
using System.Threading;
using System.Threading.Tasks;
using payment_services.Presistences;
using payment_services.Application.UseCase;
using payment_services.Domain.Entities;
using MediatR;
using System.Linq;

namespace payment_services.Application.UseCase.payment.Update
{
    public class UpdateNotificationHandler: IRequestHandler<UpdatePayment, UpdatePaymentDto>
    {
        private readonly payment_context _context;
        public UpdateNotificationHandler(payment_context context)
        {
            _context = context;
        }
        public async Task<UpdatePaymentDto> Handle(UpdatePayment request, CancellationToken cancellationToken)
        {
            var data = await _context.payments.FindAsync(request.Data.Attributes.id);
                
            await _context.SaveChangesAsync();

            return new UpdatePaymentDto()
            {
                Message = "Data successfully updated",
                Success = true,
                
            };
        }
    }
}