using System;

namespace Core
{
    public class AnotherService : IAnotherService
    {
        public AnotherService()
        {
            Statistics.AnotherServiceCount += 1;
        }

        public string Decorate(string text)
        {
            return text.Replace(", ", " ** ");
        }
    }
}