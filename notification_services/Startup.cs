using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.PostgreSql;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using notification_services.Application.UseCase.Notification.ReadBy;
using notification_services.Presistences;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace notification_services
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<notification_context>(options => options.UseNpgsql(Configuration.GetConnectionString("defaultConnection")));
            services.AddMediatR(typeof(ReadNotificationHandler).GetTypeInfo().Assembly);
            services.AddControllers();
            services.AddHangfire(config =>
                config.UsePostgreSqlStorage("Host=pg_docker;Database=aspnethangfiredb;Username=postgres;Password=docker"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHangfireServer();

            app.UseHangfireDashboard();
            RecurringJob.AddOrUpdate<RabbitListener>(x => x.Register(),Cron.Minutely);
        }
        public class RabbitListener
        {
        public void Register()
        {
            var client = new HttpClient();
            var factory = new ConnectionFactory() { HostName = "some-rabbit" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("userDataExchange", "fanout");
                channel.QueueDeclare("notification", true, false, false, null);
                channel.QueueBind("notification", "userDataExchange", string.Empty);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    var content = new StringContent(message, Encoding.UTF8, "application/json");
                    if (content != null)
                    {

                        Console.WriteLine($"Processing data from queue");
                        await client.PostAsync("http://notification-container/notification", content);
                    }
                    else
                    {
                        Console.WriteLine("No data");
                    }
                };
                channel.BasicConsume(queue: "notification",
                                     autoAck: true,
                                     consumer: consumer);

                Thread.Sleep(10000);
            }
        }
        }
    }
}
