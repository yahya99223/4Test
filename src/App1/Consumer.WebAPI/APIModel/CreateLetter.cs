using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Consumer.WebAPI.APIModel
{
    public class CreateLetter
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}