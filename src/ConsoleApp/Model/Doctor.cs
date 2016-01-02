using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Model
{
    public class Doctor : User
    {
        public Doctor(int id, string name) : base(id, name)
        {
        }


        public int Degree { get; set; }
    }
}
