using api.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;

namespace api.Model.EntityModel
{
    public class Anticipation : Entity
    {
        protected Anticipation() { }

        

        public int UniqueIdentifier { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime AnalysisStartDate { get; set; }
        public DateTime? AnalysisEndDate { get; set; }
        public string AnalysisResult { get; set; }
        public decimal RequestAmountOfAnticipation { get; set; }
        public decimal AnticipatedValue { get; set; }
        public ICollection<Transaction> Transactions { get; set; }

    }
}
