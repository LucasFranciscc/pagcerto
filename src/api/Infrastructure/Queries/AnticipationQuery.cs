using api.Model.EntityModel;
using System.Linq;

namespace api.Infrastructure.Queries
{
    public static class AnticipationQuery
    {
        public static IQueryable<Anticipation> WhereStatus(this IQueryable<Anticipation> anticipations, string status)
        {
            switch (status)
            {
                case "pending":
                    return anticipations.Where(a => a.AnalysisStartDate == null);
                case "under analysis":
                    return anticipations.Where(a => a.AnalysisStartDate != null && a.AnalysisEndDate == null);
                case "finished":
                    return anticipations.Where(a => a.AnalysisEndDate != null);
                default:
                    return anticipations;

            }
        }
    }
}
