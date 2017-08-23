using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Core;

namespace WebApp.Controllers
{
    public class TestController : ApiController
    {
        private readonly ISomeService someService;

        public TestController(ISomeService someService)
        {
            this.someService = someService;
        }

        [Route("api/test")]
        public IHttpActionResult Get()
        {
            var result = someService.SayHi();
            return Ok(result);
        }
    }
}
