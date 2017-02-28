using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Shared.Messaging;


namespace Consumer.WebAPI.Controllers
{
    public class MessageController : ApiController
    {
        [HttpPost]
        [Route("Send")]
        public IHttpActionResult Start(Letter message)
        {
            return Ok(message);
        }
    }
}
