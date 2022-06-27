using api.Model.EntityModel;
using System;

namespace api.Model.ResultModel
{
    public class AnticipationResultJson
    {
        public AnticipationResultJson(Anticipation anticipation)
        {
            UniqueIdentifier = anticipation.UniqueIdentifier;
            RequestDate = anticipation.RequestDate;
            AnalysisStartDate = anticipation.AnalysisStartDate;
            AnalysisEndDate = anticipation.AnalysisEndDate;
            AnalysisResult = anticipation.AnalysisResult.ToString().ToLower();
            RequestAmountOfAnticipation = anticipation.RequestAmountOfAnticipation;
            AnticipatedValue = anticipation.AnticipatedValue;
        }

        public int UniqueIdentifier { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? AnalysisStartDate { get; set; }
        public DateTime? AnalysisEndDate { get; set; }
        public string AnalysisResult { get; set; }
        public decimal RequestAmountOfAnticipation { get; set; }
        public decimal? AnticipatedValue { get; set; }
    }
}
