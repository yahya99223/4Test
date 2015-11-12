using System.Collections.Generic;

namespace Core.DomainModel
{
    public class CapturedMedia
    {
        public CapturedMedia()
        {
            MetaData = new Dictionary<string, string>();
        }

        public byte[] MediaData { get; set; }
        public MediaType MediaType { get; set; }
        public Dictionary<string, string> MetaData { get; set; }
    }
}