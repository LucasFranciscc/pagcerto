using api.Infrastructure;
using System;

namespace api.Model.EntityModel
{
    public class Installment : Entity
    {
        protected Installment() { }

        public Installment(int installments, decimal grossValue, decimal netValue, string receiptDate)
        {
            InstallmentIdentifier = Math.Abs(Id.GetHashCode());
            InstallmentNumber = installments;
            GrossValue = grossValue;
            NetValue = netValue;
            ReceiptDate = receiptDate;
        }

        public int InstallmentIdentifier { get; set; }
        public Guid TransactionInstallmentsId { get; set; }
        public Transaction Transaction { get; set; }
        public decimal GrossValue { get; set; }
        public decimal NetValue { get; set; }
        public int InstallmentNumber { get; set; }
        public decimal? AnticipatedValue { get; set; }
        public string ReceiptDate { get; set; }
        public DateTime? TransferDate { get; set; }

        public void ApplyAnticipation()
        {
            AnticipatedValue = NetValue - (3.8M * (NetValue / 100));
        }

        public void Transfer() => TransferDate = DateTime.Now;

    }
}
