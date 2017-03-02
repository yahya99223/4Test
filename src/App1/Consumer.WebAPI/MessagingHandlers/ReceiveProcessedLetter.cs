using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Consumer.WebAPI.DAL;
using MassTransit;
using Shared.Messaging.Events;


namespace Consumer.WebAPI.MessagingHandlers
{
    public class ReceiveProcessedLetter : IConsumer<LetterProcessed>
    {
        public async Task Consume(ConsumeContext<LetterProcessed> context)
        {
            await Task.Run(() =>
            {
                var letter = InMemoryData.Letters.FirstOrDefault(l => l.Id == context.Message.Letter.Id);
                InMemoryData.Letters.Remove(letter);
                InMemoryData.Letters.Add(context.Message.Letter);
            });
        }
    }
}