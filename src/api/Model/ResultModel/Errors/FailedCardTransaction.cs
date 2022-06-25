namespace api.Model.ResultModel.Errors
{
    public class FailedCardTransaction : ProcessError
    {
        public FailedCardTransaction()
        {
            Error = "THE_CARD_TRANSACTION_WAS_DISAPPROVED";
        }
    }
}
