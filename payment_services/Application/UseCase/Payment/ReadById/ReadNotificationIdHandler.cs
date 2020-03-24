using System.Threading;
using System.Threading.Tasks;
using payment_services.Presistences;
using payment_services.Domain.Entities;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Collections.Generic;

namespace payment_services.Application.UseCase.payment.ReadById
{
    public class ReadPaymentIdHandler : IRequestHandler<ReadPaymentId, ReadPaymentIdDto>
    {
        private readonly payment_context _context;

        public ReadPaymentIdHandler(payment_context context)
        {
            _context = context;
        }
        public async Task<ReadPaymentIdDto> Handle(ReadPaymentId request, CancellationToken cancellationToken)
        {

           var data = await _context.payments.FindAsync(request.Id);

            return new ReadPaymentIdDto()
            {
                Message = "Successfully Retrieving Data",
                Success = true,
                Data = data
            };

        }
    }
}