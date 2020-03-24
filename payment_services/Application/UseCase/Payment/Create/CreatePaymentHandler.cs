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

            var target = new TargetCommand() { Id = 3123, Email_destination = "nugi@gmail.com"};

            PostCommand command = new PostCommand()
            {
                Title = "this is simple",
                Message = "dont judge me",
                Type = "email",
                From = 1,
                Targets = new List<TargetCommand>() { target }
            };

            var attributes = new Data<PostCommand>()
            { Attributes = command };

            var httpContent = new RequestData<PostCommand>()
            { Data = attributes };

            var jsonObj = JsonConvert.SerializeObject(httpContent);

            var factory = new ConnectionFactory() {HostName = "localhost"};
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("userDataExchange", "fanout");
                channel.QueueDeclare("notification", true, false, false, null);

                channel.QueueBind("notification", "userDataExchange", string.Empty);

                var body = Encoding.UTF8.GetBytes(jsonObj);

                channel.BasicPublish(
                    exchange: "userDataExchange",
                    routingKey: "",
                    basicProperties: null,
                    body: body
                    );
                Console.WriteLine("User data has been forwarded");
                
            }
            

            return new CreatePaymentDto
            {
                Success = true,
                Message = " successfully created",
            };

        }
        
        
    }
   
}