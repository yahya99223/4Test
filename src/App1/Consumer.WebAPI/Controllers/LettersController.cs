using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Consumer.WebAPI.APIModel;
using Consumer.WebAPI.DAL;
using Shared.Messaging.Events;
using Shared.Messaging.Messages;


namespace Consumer.WebAPI.Controllers
{
    public class LettersController : ApiController
    {
        [HttpPost]
        [Route("Letter/{id}")]
        public IHttpActionResult Get(Guid id)
        {
            var letter = InMemoryData.Letters.FirstOrDefault(l => l.Id == id);
            return Ok(letter);
        }


        [HttpPost]
        [Route("Send")]
        public async Task<IHttpActionResult> Post(CreateLetter message)
        {
            var letter = new Letter(Guid.NewGuid(), message.From, message.To, message.Title, message.Body, DateTime.UtcNow, null);
            InMemoryData.Letters.Add(letter);
            var newLetterReceivedEvent = new NewLetterReceived(letter);
            await MessagingConfig.Bus.Publish(newLetterReceivedEvent);

            return Ok(letter);
        }
    }
}
