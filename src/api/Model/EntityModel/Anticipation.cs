using api.Infrastructure;
using api.Model.EntityModel.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace api.Model.EntityModel
{
    public class Anticipation : Entity
    {
        protected Anticipation() { }

        public Anticipation(List<Transaction> transaction)
        {
            UniqueIdentifier = Math.Abs(Id.GetHashCode());
            Transactions = transaction;
            RequestAmountOfAnticipation = CalculateRequestedAmount();
            AnalysisResult = AnticipationStatus.PENDING;
            RequestDate = DateTime.Now;
        }

        public int UniqueIdentifier { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? AnalysisStartDate { get; set; }
        public DateTime? AnalysisEndDate { get; set; }
        public AnticipationStatus AnalysisResult { get; set; }
        public decimal RequestAmountOfAnticipation { get; set; }
        public decimal? AnticipatedValue { get; set; }
        public ICollection<Transaction> Transactions { get; set; }

        private decimal CalculateRequestedAmount() => Transactions.Select(t => t.NetAmount).ToList().Sum();

        public void StartService() => AnalysisStartDate = DateTime.Now;

        public void CalculateAnticipatedAmount()
        {
            AnticipatedValue = Transactions
                .Where(t => t.Anticipated.HasValue && t.Anticipated.Value)
                .SelectMany(t => t.Installments)
                .Select(i => i.AnticipatedValue)
                .Sum();
        }

        public void FinalizeService()
        {
            AnalysisEndDate = DateTime.Now;

            Transactions
                .Where(t => t.Anticipated.Value)
                .SelectMany(t => t.Installments)
                .ToList().ForEach(i => i.Transfer());

            if (Transactions.ToList().Any(t => t.Anticipated.HasValue && t.Anticipated.Value))
                AnalysisResult = AnticipationStatus.PARTIALLYAPPROVED;

            if (Transactions.ToList().TrueForAll(t => t.Anticipated.HasValue && t.Anticipated.Value))
                AnalysisResult = AnticipationStatus.APPROVED;

            if (Transactions.ToList().TrueForAll(t => t.Anticipated.HasValue && !t.Anticipated.Value))
                AnalysisResult = AnticipationStatus.REPROVED;
        }

    }
}
