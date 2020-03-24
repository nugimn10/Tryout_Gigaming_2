using MediatR;
using System;
using payment_services.Domain.Entities;
using payment_services.Application.UseCase;
using System.Collections.Generic;

namespace payment_services.Application.UseCase.payment.Create
{
    public class CreatePayment : RequestData<PaymentTb>,IRequest<CreatePaymentDto>
    {

    }
    
}
