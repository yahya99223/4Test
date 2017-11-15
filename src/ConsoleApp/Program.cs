using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //getOUs();



            using (var context = new PrincipalContext(ContextType.Domain, "corp.idscan.com", "OU=Mersin,DC=corp,DC=idscan,DC=com"))
            {
                using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
                {
                    foreach (var result in searcher.FindAll())
                    {
                        DirectoryEntry de = result.GetUnderlyingObject() as DirectoryEntry;
                        foreach (var deProperty in de.Properties.PropertyNames)
                        {
                            var val = de.Properties[deProperty.ToString()]?.Value?.ToString();
                            if (!string.IsNullOrEmpty(val))
                                Console.WriteLine(deProperty + ": " + val);
                        }
                        Console.WriteLine();
                    }
                }
            }
            Console.ReadLine();
        }

        static void getOUs()
        {
            List<string> orgUnits = new List<string>();

            DirectoryEntry startingPoint = new DirectoryEntry("LDAP://DC=corp,DC=idscan,DC=com");
            var children = startingPoint.Children;
            DirectorySearcher searcher = new DirectorySearcher(startingPoint);
            searcher.Filter = "(objectCategory=organizationalUnit)";

            foreach (SearchResult res in searcher.FindAll()) 
            {
                orgUnits.Add(res.Path);
                Console.WriteLine(res.Path);
            }
        }
    }
}
