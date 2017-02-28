using System.Web.Http;


namespace Producer.WebAPI.Controllers
{
    public class ProcessController : ApiController
    {
        [HttpPost]
        [Route("Process/{message}")]
        public IHttpActionResult Start(string message)
        {
            return Ok(message);
        }
    }
}
