using api.Infrastructure;
using api.Model.EntityModel;
using api.Model.EntityModel.Interface;
using api.Model.IntegrationModel;
using api.Model.ResultModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace api.Model.ServiceModel
{
    public class PaymentProcessing
    {
        private readonly PagCertoDbContext _dbContext;

        private const decimal FlatRate = 0.9M;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICardPaymentRepository _cardPaymentRepository;

        private const string RejectedCard = "5999";

        public PaymentProcessing(PagCertoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Transaction transaction { get; set; }
        public bool Reproved { get; set; }

        public async Task<Transaction> Process(CardPayment cardPayment)
        {
            var transaction = new Transaction(cardPayment.CardNumber, cardPayment.Amount);

            var card = cardPayment.CardNumber.Substring(0, 4);

            if (card == RejectedCard)
            {
                transaction.Failure();
            }
            else
            {
                transaction.Approve();
                transaction.calculateRate(FlatRate);
                transaction.CreatePlots(cardPayment.Installments);
                transaction.NumberOfInstallments = cardPayment.Installments;
            }

            Reproved = !transaction.IsApproved();

            if (Reproved)
            {
                _dbContext.Add(transaction);
                await _dbContext.SaveChangesAsync();
                return transaction;
            }

            _dbContext.Add(transaction);
            await _dbContext.SaveChangesAsync();
            return transaction;
        }
    }
}
