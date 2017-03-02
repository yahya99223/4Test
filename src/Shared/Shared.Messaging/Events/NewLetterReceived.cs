using Shared.Messaging.Messages;


namespace Shared.Messaging.Events
{
    public class NewLetterReceived
    {
        public NewLetterReceived(ILetter letter)
        {
            Letter = letter;
        }


        public ILetter Letter { get; private set; }
    }
}