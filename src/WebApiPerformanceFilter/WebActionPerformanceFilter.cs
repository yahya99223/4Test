using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WebActionPerformance;

namespace WebApiPerformanceFilter
{
    public class WebActionPerformanceFilter : ActionFilterAttribute
    {
        private DateTime startTime;
        private DateTime endTime;

        public WebActionPerformanceFilter()
        {
            this.startTime = DateTime.UtcNow;
            this.endTime = DateTime.UtcNow;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            startTime = DateTime.UtcNow;
            base.OnActionExecuting(actionContext);
        }

        /*public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            startTime = DateTime.UtcNow;
            return base.OnActionExecutingAsync(actionContext, cancellationToken);
        }*/

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            endTime = DateTime.UtcNow;
            var duration =endTime.Ticks - startTime.Ticks;

            if (actionExecutedContext.Response.StatusCode == HttpStatusCode.BadRequest)
                PerformanceCounterLocator.Instance.WebActionOperationError.RecordOperation((long) duration);
            else
                PerformanceCounterLocator.Instance.WebActionOperation.RecordOperation((long) duration);

            base.OnActionExecuted(actionExecutedContext);
        }

        /*public override Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            endTime = DateTime.UtcNow;
            PerformanceCounterLocator.Instance.WebActionOperation.RecordOperation(endTime.Ticks - startTime.Ticks);
            return base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
        }*/
    }
}
