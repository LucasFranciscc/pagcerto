using api.Infrastructure;

namespace api.Model.EntityModel.Interface
{
    public interface ICardPaymentRepository
    {
        Transaction Process(CardPayment cardPayment);
    }
}
