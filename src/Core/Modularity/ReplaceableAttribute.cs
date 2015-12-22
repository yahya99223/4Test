using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modularity
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ReplaceableAttribute : Attribute
    {
        public ReplaceableAttribute(string myName)
        {
            MyName = myName;
        }

        public string MyName { get; private set; }
    }



    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ReplaceAttribute : Attribute
    {
        public ReplaceAttribute(params string[] replaceList)
        {
            ReplaceList = replaceList;
        }

        public string[] ReplaceList { get; private set; }
    }
}
