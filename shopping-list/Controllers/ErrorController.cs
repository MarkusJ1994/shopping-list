using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shopping_list;

namespace shopping_list
{
	[ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorController : ControllerBase
	{
        [Route("/error")]
        public IActionResult Error()
		{
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>()!;
            var exception = context.Error; // Your exception
            var code = 500;

            if (exception is HttpStatusException httpException)
            {
                code = (int)httpException.Status;
            }

            return Problem(statusCode: code, title: exception.Message);
        }
	}
}

