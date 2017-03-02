using Shared.Messaging.Messages;


namespace Shared.Messaging.Events
{
    public class LetterProcessed
    {
        public LetterProcessed(ILetter letter)
        {
            Letter = letter;
        }


        public ILetter Letter { get; private set; }
    }
}