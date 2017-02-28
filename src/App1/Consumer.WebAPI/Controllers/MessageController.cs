using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace Consumer.WebAPI.Controllers
{
    public class MessageController : ApiController
    {
        [HttpPost]
        [Route("Send/{message}")]
        public IHttpActionResult Start(string message)
        {
            return Ok(message);
        }
    }
}
