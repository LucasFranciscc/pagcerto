using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace api.Model.ResultModel.Errors
{
    public class ProcessError : IActionResult
    {
        public string Error { get; set; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var json = new JsonResult(this) { StatusCode = 422 };
            await json.ExecuteResultAsync(context);
        }
    }
}
