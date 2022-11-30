using System.Net;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace CQRS_with_Redis.ActionFilter
{
    public class BadRequestActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Response = actionContext.Request.CreateResponse
                    (HttpStatusCode.BadRequest, new ValidationErrorWrapper(actionContext.ModelState));
            }
            base.OnActionExecuting(actionContext);
        }
    }
}
