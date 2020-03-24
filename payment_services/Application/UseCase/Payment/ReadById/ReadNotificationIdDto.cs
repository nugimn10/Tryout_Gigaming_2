using payment_services.Domain.Entities;
using payment_services.Application.UseCase;

namespace payment_services.Application.UseCase.payment.ReadById
{
    public class ReadPaymentIdDto : BaseDto
    {
        public PaymentTb Data { get; set; }
    }
}