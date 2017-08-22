using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApp.Controllers
{
    public class TestController : ApiController
    {
        [Route("api/test")]
        public IHttpActionResult Get()
        {
            return Ok("API is OK");
        }
    }
}
