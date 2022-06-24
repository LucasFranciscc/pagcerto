using api.Infrastructure;

namespace api.Model.EntityModel
{
    public class CardPayment : Entity
    {
        public string CardNumber { get; set; }
        public decimal Amount { get; set; }
        public int Installments { get; set; }
    }
}
