using api.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api.Model.EntityModel
{
    public class Transaction : Entity
    {
        protected Transaction() { }

        public Transaction(string card, decimal amount)
        {
            Nsu = Math.Abs(Id.GetHashCode());
            CardFinal = card.Substring(12);
            GrossAmount = amount;
            Installments = new List<Installment>();
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
        public ICollection<Installment> Installments { get; set; }
        //public int InstallmentsCount => Installments.Count;

        public void Approve()
        {
            TransactionDatePerformed = DateTime.Now;
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

        public void CreatePlots(int installment)
        {
            if (installment == 0) return;

            var grossValue = (GrossAmount / installment);
            var netValue = (NetAmount / installment);

            for (int installmentNumber = 1; installmentNumber <= installment; installmentNumber++)
            {
                Installments.Add(new Installment(
                    installmentNumber,
                    grossValue,
                    netValue,
                    ApprovalDate.Value.AddDays(installmentNumber * 30).ToString("yyyy-MM-dd")));
            }

        }
    }
}
