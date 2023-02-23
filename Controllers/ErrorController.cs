using ECommerce_App.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce_App.Controllers
{


    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : BaseApiController
    {

        public IActionResult Error(int code)
        {

            return new ObjectResult(new ApiResponse(code));

        }
         
    }
}
