using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace IssueTracker.Web.Infrastructure.Filters
{
    public class ValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var modelState = actionContext.ModelState;

            if (!modelState.IsValid)
            {
                var errors = modelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .Select(e => new
                    {
                        Property = e.Key,
                        Message = e.Value.Errors.First().ErrorMessage
                    }).ToArray();

                actionContext.Response =
                    actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, errors);

            }
            else
            {
                base.OnActionExecuting(actionContext);
            }
        }
    }
}