using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modularity
{
    public interface IReplaceable
    {
    }



    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class NameAttribute : Attribute
    {
        public string Name { get; private set; }


        public NameAttribute(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("name");

            Name = name;
        }
    }


    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ReplaceForAttribute : Attribute
    {
        public string TargetName { get; private set; }
        public string[] Businesses { get; private set; }


        public ReplaceForAttribute(string targetName = null, params string[] businesses)
        {
            TargetName = targetName;
            Businesses = businesses.Select(x=>x.ToLower()).ToArray();
        }
    }
}