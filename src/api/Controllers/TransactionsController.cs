using api.Model.ResultModel;
using api.Model.ResultModel.Errors;
using api.Model.ServiceModel;
using api.Model.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace api.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class TransactionsController : Controller
    {
        private readonly PaymentProcessing _paymentProcessing;

        public TransactionsController(
            PaymentProcessing paymentProcessing)
        {
            _paymentProcessing = paymentProcessing;
        }

        [HttpPost, Route("create")]
        public async Task<IActionResult> Create([FromBody] CreatePaymentViewModel model)
        {
            await _paymentProcessing.Process(model.ToCardPayment());

            if (_paymentProcessing.Reproved)
                return new FailedCardTransaction();

            return Ok(new TransactionResultJson(_paymentProcessing.transaction));
        }
    }
}
