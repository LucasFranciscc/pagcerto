using api.Infrastructure;
using api.Infrastructure.Queries;
using api.Model.ResultModel;
using api.Model.ResultModel.Errors;
using api.Model.ServiceModel;
using api.Model.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class AnticipationController : Controller
    {
        private readonly AnticipationProcess _anticipationProcess;
        private readonly PagCertoDbContext _context;

        public AnticipationController(
            AnticipationProcess anticipationProcess,
            PagCertoDbContext context)
        {
            _anticipationProcess = anticipationProcess;
            _context = context;
        }

        [HttpGet, Route("pending-anticipations")]
        public async Task<IActionResult> listPendingAnticipations()
        {
            var transactions = await _context.Transactions
                .IncludeInstallments()
                .AvailableAnticipations()
                .ToListAsync();

            var transactionResultJson = transactions.Select(t => new TransactionResultJson(t)).ToList();

            return Ok(transactionResultJson);
        }

        [HttpGet, Route("anticipation-history")]
        public async Task<IActionResult> AnticipationHistory([FromQuery] string status)
        {
            var anticipations = await _context.Anticipations
                .WhereStatus(status)
                .ToListAsync();

            var anticipationsJson = anticipations.Select(a => new AnticipationResultJson(a)).ToList();
            return Ok(anticipationsJson);
        }

        [HttpPost, Route("create-anticipation")]
        public async Task<IActionResult> create([FromBody] AnticipationViewModel model)
        {
            await _anticipationProcess.Process(model.Nsus);

            if (_anticipationProcess.InProgress) return new AnticipationInProgress();

            if (_anticipationProcess.RequestedAdvance) return new AlreadyRequestedAnticipation();

            return Ok(new AnticipationResultJson(_anticipationProcess.anticipation));
        }

        [HttpPut, Route("start-service")]
        public async Task<IActionResult> StartService()
        {
            var antecipation = await _context.Anticipations
                .WhereStatus("pending")
                .SingleOrDefaultAsync();

            if (antecipation == null) return new AnticipationNotFound();

            antecipation.StartService();

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut, Route("approve")]
        public async Task<IActionResult> Approve([FromBody] AnticipationViewModel model)
        {
            await _anticipationProcess.Approve(model.Nsus);

            if (_anticipationProcess.NotFound) return new AnticipationNotFound();
            if (_anticipationProcess.VerifiedTransaction) return new TransactionAlreadyAnalyzed();

            return NoContent();
        }

        [HttpPut, Route("reprove")]
        public async Task<IActionResult> Reprove([FromBody] AnticipationViewModel model)
        {
            await _anticipationProcess.Reprove(model.Nsus);

            if (_anticipationProcess.NotFound) return new AnticipationNotFound();
            if (_anticipationProcess.VerifiedTransaction) return new TransactionAlreadyAnalyzed();

            return NoContent();
        }

    }
}
