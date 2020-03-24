using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using payment_services.Presistences;
using System.Linq;
using payment_services.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using MediatR;
using RabbitMQ.Client;
using System;
using RabbitMQ.Client.Events;
using System.Text;
using System.Net.Http;

namespace payment_services.Application.UseCase.payment.ReadBy
{
    public class ReadPaymentHandler : IRequestHandler<ReadPayment, ReadpaymentDto>
    {
        private readonly payment_context _context;

        public ReadPaymentHandler(payment_context context)
        {
            _context = context;
        }
        public async Task<ReadpaymentDto> Handle(ReadPayment request, CancellationToken cancellationToken)
        {
        
            var data = await _context.payments.ToListAsync();
            

            return new ReadpaymentDto
            {
                Message = "Data successfully retrieved",
                Success = true,
                Data = data
            };

        }

    }
}
