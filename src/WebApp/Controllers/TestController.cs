using System.Threading.Tasks;
using System.Web.Http;

namespace WebApp.Controllers
{
    public class TestController : ApiController
    {
        [HttpGet]
        [Route("api/Test")]
        public async Task<IHttpActionResult> Get()
        {
            return Ok("API IS WORKING");
        }
    }
}