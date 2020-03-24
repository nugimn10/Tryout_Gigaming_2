using MediatR;
using System;
using notification_services.Domain.Entities;
using notification_services.Application.UseCase;

namespace notification_services.Application.UseCase.Notification.Delete
{
    public class DeleteNotification : IRequest<DeleteNotificationDto>
    {
        public int Id { get; set; }
        public DeleteNotification(int id)
        {
            Id = id;
        }

    }
}