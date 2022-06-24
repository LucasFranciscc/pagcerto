using api.Model.EntityModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace api.Model.ResultModel
{
    public class TransactionResultJson : IActionResult
    {
        public TransactionResultJson() { }

        public TransactionResultJson(Transaction transaction)
        {
            Nsu = transaction.Nsu;
            TransactionDatePerformed = transaction.TransactionDatePerformed;
            ApprovalDate = transaction.ApprovalDate;
            FailureDate = transaction.FailureDate;
            Anticipated = transaction.Anticipated;
            AcquirerConfirmation = transaction.AcquirerConfirmation;
            GrossAmount = transaction.GrossAmount;
            NetAmount = transaction.NetAmount;
            FlatRate = transaction.FlatRate;
            NumberOfInstallments = transaction.NumberOfInstallments;
            CardFinal = transaction.CardFinal;

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

        public Task ExecuteResultAsync(ActionContext context)
        {
            return new JsonResult(this).ExecuteResultAsync(context);
        }
    }
}
