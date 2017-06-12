using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Micro.Shared.Messaging
{
    public class ProcessTextCommand
    {
        public string Text { get; set; }
        public int ProcessComplexity { get; set; }
    }
}
