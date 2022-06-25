using api.Infrastructure;
using api.Infrastructure.Queries;
using api.Model.ResultModel;
using api.Model.ResultModel.Errors;
using api.Model.ServiceModel;
using api.Model.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace api.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class TransactionsController : Controller
    {
        private readonly PaymentProcessing _paymentProcessing;
        private readonly PagCertoDbContext _context;

        public TransactionsController(
            PaymentProcessing paymentProcessing,
            PagCertoDbContext context)
        {
            _paymentProcessing = paymentProcessing;
            _context = context;
        }

        [HttpGet, Route("find/{nsu:int}")]
        public async Task<IActionResult> Find([FromRoute] int nsu)
        {
            var transaction = await _context.Transactions
                .IncludeInstallments()
                .WhereNsu(nsu)
                .SingleOrDefaultAsync();

            if (transaction == null) return NotFound();

            return Ok(new TransactionResultJson(transaction));
        }

        [HttpPost, Route("create")]
        public async Task<IActionResult> Create([FromBody] CreatePaymentViewModel model)
        {
            var transaction = await _paymentProcessing.Process(model.ToCardPayment());

            if (_paymentProcessing.Reproved)
                return new FailedCardTransaction();


            return Ok(new TransactionResultJson(transaction));
        }
    }
}
