using api.Model.EntityModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace api.Model.ResultModel
{
    public class InstallmentResultJson : IActionResult
    {
        public InstallmentResultJson() { }

        public InstallmentResultJson(Installment installment)
        {
            InstallmentIdentifier = installment.InstallmentIdentifier;
            TransactionId = installment.TransactionId;
            GrossValue = installment.GrossValue;
            NetValue = installment.NetValue;
            InstallmentNumber = installment.InstallmentNumber;
            AnticipatedValue = installment.AnticipatedValue;
            ReceiptDate = installment.ReceiptDate;
            TransferDate = installment.TransferDate;
        }

        public int InstallmentIdentifier { get; set; }
        public Guid TransactionId { get; set; }
        public decimal GrossValue { get; set; }
        public decimal NetValue { get; set; }
        public int InstallmentNumber { get; set; }
        public decimal? AnticipatedValue { get; set; }
        public string ReceiptDate { get; set; }
        public DateTime? TransferDate { get; set; }

        public Task ExecuteResultAsync(ActionContext context)
        {
            return new JsonResult(this).ExecuteResultAsync(context);
        }
    }
}
