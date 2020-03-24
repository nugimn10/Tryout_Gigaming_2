using System.Text;
using System.Security.Cryptography.X509Certificates;
using System;
using System.Threading;
using System.Threading.Tasks;
using payment_services.Domain.Entities;
using payment_services.Presistences;
using MediatR;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Linq;
using MimeKit;
using System.Collections.Generic;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace payment_services.Application.UseCase.payment.Create
{
    public class CreatePaymentHandler : IRequestHandler<CreatePayment, CreatePaymentDto>
    {
        private readonly payment_context _context;

        public CreatePaymentHandler (payment_context context)
        {
            _context = context;
        }
        public async Task<CreatePaymentDto> Handle(CreatePayment request, CancellationToken cancellationToken)
        {
            var notificationList = _context.payments.ToList();

            var paydata = new PaymentTb()
            {
                Payment_type = request.Data.Attributes.Payment_type,
                gross_amout = request.Data.Attributes.gross_amout,
                Order_id = request.Data.Attributes.Order_id
                
            };
  
            await _context.SaveChangesAsync();

            return new CreatePaymentDto
            {
                Success = true,
                Message = " successfully created",
            };

        }
        
        
    }
   
}