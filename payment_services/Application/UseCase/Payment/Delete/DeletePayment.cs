using MediatR;
using System;
using payment_services.Domain.Entities;
using payment_services.Application.UseCase;

namespace payment_services.Application.UseCase.payment.Delete
{
    public class Deletepayment : IRequest<DeletePaymentDto>
    {
        public int Id { get; set; }
        public Deletepayment(int id)
        {
            Id = id;
        }

    }
}