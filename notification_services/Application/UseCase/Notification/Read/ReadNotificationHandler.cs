using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using notification_services.Presistences;
using System.Linq;
using notification_services.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using MediatR;
using RabbitMQ.Client;
using System;
using RabbitMQ.Client.Events;
using System.Text;
using System.Net.Http;

namespace notification_services.Application.UseCase.Notification.ReadBy
{
    public class ReadNotificationHandler : IRequestHandler<ReadNotification, ReadNotificationDto>
    {
        private readonly notification_context _context;

        public ReadNotificationHandler(notification_context context)
        {
            _context = context;
        }
        public async Task<ReadNotificationDto> Handle(ReadNotification request, CancellationToken cancellationToken)
        {
            var client = new HttpClient();
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using(var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("userDataExchange", "fanout");
                
                channel.QueueDeclare("userData", true, false, false, null);

                channel.QueueBind("userData", "userDataExchange", string.Empty);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    var content = new StringContent(message, Encoding.UTF8, "application/json");
                    Console.WriteLine($"Processing data from queue");
                    
                    await client.PostAsync("http://localhost:1000/notification", content);
                    
                };
                channel.BasicConsume(queue: "userData",
                                    autoAck: true,
                                    consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        
            var data = await _context.Notification.ToListAsync();
            var result = new List<NotificationData>();

            foreach (var x in data)
            {
                result.Add(new NotificationData {
                    Id = x.id,
                    Title = x.title,
                    Message = x.message
                });
            }
            

            return new ReadNotificationDto
            {
                Message = "Message successfully retrieved",
                Success = true,
               
                Data = result
            };

        }

    }
}
