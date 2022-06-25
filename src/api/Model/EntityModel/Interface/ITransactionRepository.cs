using api.Infrastructure;

namespace api.Model.EntityModel.Interface
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        void Add(Transaction transaction);
    }
}
