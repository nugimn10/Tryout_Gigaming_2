using MediatR;

namespace payment_services.Application.UseCase.payment.ReadById
{

    public class ReadPaymentId: IRequest<ReadPaymentIdDto>
    {
        public int Id { get; set; }
        public ReadPaymentId(int id)
        {
            Id = id;
        }
    }
}