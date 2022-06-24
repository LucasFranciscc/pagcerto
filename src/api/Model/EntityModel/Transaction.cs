using api.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace api.Model.EntityModel
{
    public class Transaction : Entity
    {
        protected Transaction() { }
        
        public Transaction(string card, decimal amount, int numberOfInstallments)
        {
            Nsu = Math.Abs(Id.GetHashCode());
            CardFinal = card.Substring(12);
            GrossAmount = amount;
            NumberOfInstallments = numberOfInstallments;
        }

        public int Nsu { get; set; }
        public DateTime TransactionDatePerformed { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public DateTime? FailureDate { get; set; }
        public bool? Anticipated { get; set; }
        public string AcquirerConfirmation { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal FlatRate { get; set; }
        public int NumberOfInstallments { get; set; }
        public string CardFinal { get; set; }

        public void Approve()
        {
            ApprovalDate = DateTime.Now;
            AcquirerConfirmation = "Approved";
        }

        public void Failure()
        {
            TransactionDatePerformed = DateTime.Now;
            FailureDate = DateTime.Now;
            AcquirerConfirmation = "Refused";
        }

        public bool IsApproved() => ApprovalDate != null;

        public void calculateRate(decimal flatRate)
        {
            FlatRate = flatRate;
            NetAmount = GrossAmount - FlatRate;
        }
    }
}
