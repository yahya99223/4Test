using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderManagement.DbModel;

namespace OrderManagement.ViewModel
{
   public class ProcessResultViewModel
    {
        public string Result { get; set; }
        public bool IsValid { get; set; }
        public string ServiceName { get; set; }

    }
}
