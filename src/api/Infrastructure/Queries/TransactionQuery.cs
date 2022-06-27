using api.Model.EntityModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace api.Infrastructure.Queries
{
    public static class TransactionQuery
    {
        public static IQueryable<Transaction> IncludeInstallments(this IQueryable<Transaction> transactions)
        {
            return transactions.Include(t => t.Installments);
        }

        public static IQueryable<Transaction> WhereNsu(this IQueryable<Transaction> transactions, int nsu)
        {
            var response = transactions.Where(t => t.Nsu == nsu);

            return response;
        }

        public static IQueryable<Transaction> AvailableAnticipations(this IQueryable<Transaction> transactions)
        {
            return transactions.Where(t => t.Anticipated == null);
        }
    }
}
