using api.Model.EntityModel;

namespace api.Model.IntegrationModel
{
    public interface PagCertoApi
    {
        private const string RejectedCard = "5999";

        Transaction Process(string CardNumber, decimal Amount, int Installments)
        {
            var transaction = new Transaction(CardNumber, Amount);

            var card = CardNumber.Substring(12);

            if (card == RejectedCard)
            {
                transaction.Failure();
                return transaction;
            }

            transaction.Approve();
            return transaction;
        }

    }
}
