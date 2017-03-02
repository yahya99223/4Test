using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Consumer.WebAPI.APIModel;
using Consumer.WebAPI.DAL;
using Consumer.WebAPI.Model;
using Shared.Messaging.Events;


namespace Consumer.WebAPI.Controllers
{
    public class LettersController : ApiController
    {
        [HttpGet]
        [Route("Letters/{id}")]
        public IHttpActionResult Get(Guid id)
        {
            var letter = InMemoryData.Letters.FirstOrDefault(l => l.Id == id);
            return Ok(letter);
        }


        [HttpPost]
        [Route("Letters/Send")]
        public async Task<IHttpActionResult> Post(CreateLetter message)
        {
            var letter = new Letter(Guid.NewGuid(), message.From, message.To, message.Title, message.Body, null, null);
            InMemoryData.Letters.Add(letter);
            var newLetterReceivedEvent = new NewLetterReceived()
            {
                Id = letter.Id,
                Body = letter.Body,
                From = letter.From,
                To = letter.To,
                Title = letter.Title
            };
            await MessagingConfig.Bus.Publish(newLetterReceivedEvent);

            return Ok(letter);
        }
    }
}
