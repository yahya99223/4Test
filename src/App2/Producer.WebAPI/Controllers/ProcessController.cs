using System.Web.Http;
using Shared.Messaging;


namespace Producer.WebAPI.Controllers
{
    public class ProcessController : ApiController
    {
        [HttpPost]
        [Route("Process")]
        public IHttpActionResult Start(Letter message)
        {
            return Ok(message);
        }
    }
}
