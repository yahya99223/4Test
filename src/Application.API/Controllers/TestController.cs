using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Application.API.Controllers
{
    public class TestController : ApiController
    {
        [Route("api/Test")]
        public IHttpActionResult Get()
        {
            return Ok("API is working");
        }
    }
}
