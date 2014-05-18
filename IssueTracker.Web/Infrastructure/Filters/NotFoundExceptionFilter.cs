using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using IssueTracker.Entities.Exceptions;

namespace IssueTracker.Web.Infrastructure.Filters
{
    public class NotFoundExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception.GetType() == typeof (EntityNotFoundException))
            {
                actionExecutedContext.Response =
                    actionExecutedContext.Request.CreateResponse(HttpStatusCode.NotFound, actionExecutedContext.Exception.Message);
            }
            else
            {
                base.OnException(actionExecutedContext);
            }
        }
    }
}