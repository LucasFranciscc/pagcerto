using api.Infrastructure;
using api.Model.EntityModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Model.ServiceModel
{
    public class AnticipationProcess
    {
        private readonly PagCertoDbContext _context;

        public AnticipationProcess(PagCertoDbContext context)
        {
            _context = context;
        }

        public Anticipation anticipation { get; set; }
        public bool InProgress { get; set; }
        public List<Transaction> Transaction { get; set; }
        public bool RequestedAdvance { get; set; }

        public List<int> TransactionNsus { get; set; }
        public Anticipation Anticipation { get; set; }
        public bool NotFound { get; set; }
        public bool VerifiedTransaction { get; set; }

        public async Task<bool> Process(List<int> Nsus)
        {
            anticipation = await FindAnticipation();

            InProgress = AnticipationInProgress();

            if (InProgress) return false;

            Transaction = await ListByNsus(Nsus);

            RequestedAdvance = RequestedAnticipation();

            if (RequestedAdvance) return false;

            anticipation = new Anticipation(Transaction);

            _context.Anticipations.Add(anticipation);
            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<bool> Approve(List<int> Nsus)
        {
            TransactionNsus = Nsus;

            await GetAnticipation();

            if (NotFound) return false;

            VerifiedTransactions();

            if (VerifiedTransaction) return false;

            ApproveRequestedTransactions();
            Anticipation.CalculateAnticipatedAmount();

            if (AnalyzedTransactions()) Anticipation.FinalizeService();

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Reprove(List<int> Nsus)
        {
            TransactionNsus = Nsus;

            await GetAnticipation();

            if (NotFound) return false;

            VerifiedTransactions();

            if (VerifiedTransaction) return false;

            ReproveRequestedTransactions();

            if (AnalyzedTransactions()) Anticipation.FinalizeService();

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Anticipation> FindAnticipation()
        {
            return await _context.Anticipations
            .Include(a => a.Transactions)
            .ThenInclude(ct => ct.Installments)
            .Where(a => a.AnalysisEndDate == null)
            .FirstOrDefaultAsync();
        }

        public bool AnticipationInProgress()
        {
            return anticipation != null && anticipation.AnalysisEndDate == null;
        }

        public async Task<List<Transaction>> ListByNsus(List<int> nsus)
        {
            return await _context.Transactions
                .Include(t => t.Installments)
                .Where(t => nsus.Contains(t.Nsu))
                .ToListAsync();
        }

        public bool RequestedAnticipation()
        {
            return Transaction.Where(t => t.RequestedAdvance()).Any();
        }

        private async Task GetAnticipation()
        {
            Anticipation = await FindAnticipation();
            if (Anticipation == null) NotFound = true;
        }

        private void VerifiedTransactions()
        {
            VerifiedTransaction = Anticipation.Transactions
                .Where(t => TransactionNsus.Contains(t.Nsu))
                .Where(t => t.Anticipated.HasValue)
                .Any();
        }

        private void ApproveRequestedTransactions()
        {
            Anticipation.Transactions
                .Where(t => TransactionNsus.Contains(t.Nsu))
                .ToList()
                .ForEach(t => t.ApproveAnticipation());
        }

        private bool AnalyzedTransactions()
        {
            return Anticipation.Transactions
                .ToList()
                .TrueForAll(t => t.Anticipated != null);
        }

        private void ReproveRequestedTransactions()
        {
            Anticipation.Transactions
                .Where(t => TransactionNsus.Contains(t.Nsu))
                .ToList().ForEach(t => t.ReproveAnticipation());
        }
    }
}
