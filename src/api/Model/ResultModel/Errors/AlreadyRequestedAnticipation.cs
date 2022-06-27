namespace api.Model.ResultModel.Errors
{
    public class AlreadyRequestedAnticipation : ProcessError
    {
        public AlreadyRequestedAnticipation()
        {
            Error = "ALREADY_REQUESTED_ANTICIPATION";
        }
    }
}
