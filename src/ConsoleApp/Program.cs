using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the full name of the domain e.g.");
            Console.WriteLine("corp.mycompany.com");

            var fullDomainString = Console.ReadLine();
            if (string.IsNullOrEmpty(fullDomainString))
                throw new Exception("Domain Full Name is required!");

            var fullDomainParts = fullDomainString.Split('.');
            var domainContainer = string.Join(",", fullDomainParts.Select(x => "DC=" + x));

            string ou = null;
            Console.WriteLine("Please enter the name of OU 'Organization Unit'");
            Console.WriteLine("Incase you want to know all OUs in your domain enter : INVESTIGATE");
            var temp = Console.ReadLine();
            if (temp == "INVESTIGATE")
            {
                getOUs(domainContainer);
                Console.WriteLine();
                Console.WriteLine("Please enter the name of OU 'Organization Unit'");
                ou = Console.ReadLine();
            }
            else
            {
                ou = temp;
            }

            if (!string.IsNullOrEmpty(ou))
                domainContainer = "OU=" + ou + "," + domainContainer;

            //using (var context = new PrincipalContext(ContextType.Domain, "corp.idscan.com", "OU=London,DC=corp,DC=idscan,DC=com"))
            using (var context = new PrincipalContext(ContextType.Domain, fullDomainString, domainContainer))
            {
                using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
                {
                    foreach (var result in searcher.FindAll().Take(1))
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

        static void getOUs(string domainContainer)
        {
            List<string> orgUnits = new List<string>();

            //DirectoryEntry startingPoint = new DirectoryEntry("LDAP://DC=corp,DC=idscan,DC=com");
            var startingPoint = new DirectoryEntry("LDAP://" + domainContainer);
            DirectorySearcher searcher = new DirectorySearcher(startingPoint)
            {
                Filter = "(objectCategory=organizationalUnit)"
            };

            foreach (SearchResult res in searcher.FindAll())
            {
                orgUnits.Add(res.Path);
                Console.WriteLine(res.Path);
            }
        }
    }

    public class User
    {
        public User()
        {
            Properties = new List<DirectoryProperty>();
        }

        public string Name { get; set; }
        public ICollection<DirectoryProperty> Properties { get; set; }
    }

    public class DirectoryProperty
    {
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
    }
}