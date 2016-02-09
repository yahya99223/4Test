using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class MainClass
    {
        public readonly IList<SubClassOne> list;


        public MainClass(IList<SubClassOne> list = null)
        {
            this.list = list ?? new List<SubClassOne>();
        }


        public SubClassOne[] SubClassOnes
        {
            get { return list.ToArray(); }
        }
    }



    public class SubClassOne
    {
        public readonly IList<SubClassTwo> list;


        public SubClassOne(IList<SubClassTwo> list = null)
        {
            this.list = list ?? new List<SubClassTwo>();
        }


        public SubClassTwo[] SubClassTwos
        {
            get { return list.ToArray(); }
        }
    }



    public class SubClassTwo
    {
        public SubClassTwo(Guid id)
        {
            Id = id;
        }


        public Guid Id { get; private set; }
    }
}
