using api.Infrastructure;
using System;

namespace api.Model.EntityModel
{
    public class Installment : Entity
    {
        protected Installment() { }

        public Installment(int installments)
        {
            InstallmentIdentifier = Math.Abs(Id.GetHashCode());
        }

        public int InstallmentIdentifier { get; set; }
        public Guid TransactionId { get; set; }
        public Transaction Transaction { get; set; }
        public decimal GrossValue { get; set; }
        public decimal NetValue { get; set; }
        public int InstallmentNumber { get; set; }
        public decimal? AnticipatedValue { get; set; }
        public DateTime ReceiptDate { get; set; }
        public DateTime TransferDate { get; set; }

    }
}
