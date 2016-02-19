using SEMS.Infrastructure.Logging;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace SEMS.WebApi.Filters
{
    public class UnifyExceptionFilter : ExceptionFilterAttribute
    {
        
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var exception = actionExecutedContext.Exception;
            var request = actionExecutedContext.Request;
            if (exception.Data.Contains("TipForUI"))
            {
                var errorInfo = exception.Data["TipForUI"];
                var httpError = new HttpError(exception.Message);
                httpError["ErrorInfo"] = errorInfo;
                var response = request.CreateErrorResponse(HttpStatusCode.BadRequest, httpError);
                actionExecutedContext.Response = response;

                LogManager.GetLogger(typeof(UnifyExceptionFilter)).Error(exception.Message, exception);
            }
        }
    }
}
