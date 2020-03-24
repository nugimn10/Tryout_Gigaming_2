using payment_services.Application.UseCase;
using payment_services.Domain.Entities;
using System;
using MediatR;
using System.Collections.Generic;

namespace payment_services.Application.UseCase.payment.Update
{
    public class UpdatePayment : RequestData<PaymentTb>,IRequest<UpdatePaymentDto>
    {

    }

}