using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace ASPwebappEmpty
{
    public class ErrorController: Controller
    {
        private ILogger<ErrorController> logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }

        [Route("/Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404: ViewBag.Message = "Page does not exists";
                    logger.LogWarning($"404 error {statusCode}");
                    break;
            }
            return View("PageNotFound");
        }

        [Route("Error")]
        [AllowAnonymous]
        public IActionResult globalExceptionHandler()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.Message = exceptionDetails.Error.Message;

            logger.LogError($"The Path {exceptionDetails.Path} threw an exception {exceptionDetails.Error}");

            return View("PageNotFound");
        }
    }
}
