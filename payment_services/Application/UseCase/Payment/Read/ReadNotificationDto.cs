using payment_services.Domain.Entities;
using payment_services.Application.UseCase;
using System.Collections.Generic;

namespace payment_services.Application.UseCase.payment.ReadBy
{
    public class ReadpaymentDto : BaseDto
    {
        public IList<PaymentTb> Data { get; set; }
    }
}