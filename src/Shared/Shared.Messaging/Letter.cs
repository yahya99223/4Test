using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Shared.Messaging
{
    public class Letter
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime SendingDate { get; set; }
    }
}
