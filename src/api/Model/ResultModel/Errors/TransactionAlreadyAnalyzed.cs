namespace api.Model.ResultModel.Errors
{
    public class TransactionAlreadyAnalyzed : ProcessError
    {
        public TransactionAlreadyAnalyzed()
        {
            Error = "TRANSACTION_ALREADY_ANALYZED";
        }
    }
}
