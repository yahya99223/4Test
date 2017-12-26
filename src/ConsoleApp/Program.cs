using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
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

                Console.WriteLine("Please enter how many users you want to pull from AD");

                if (!int.TryParse(Console.ReadLine(), out var usersCount))
                    throw new Exception("Users count should be a valid integer");

                var usersList = new List<User>();

                //using (var context = new PrincipalContext(ContextType.Domain, "corp.idscan.com", "OU=London,DC=corp,DC=idscan,DC=com"))
                using (var context = new PrincipalContext(ContextType.Domain, fullDomainString, domainContainer))
                {
                    using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
                    {
                        foreach (var result in searcher.FindAll().Take(usersCount))
                        {
                            DirectoryEntry de = result.GetUnderlyingObject() as DirectoryEntry;

                            var user = new User()
                            {
                                Name = de.Name.Replace("CN=", "")
                            };
                            foreach (var deProperty in de.Properties.PropertyNames)
                            {
                                var val = de.Properties[deProperty.ToString()]?.Value;
                                if (val != null)
                                {
                                    user.Properties.Add(new DirectoryProperty
                                    {
                                        PropertyName = deProperty.ToString(),
                                        PropertyType = val.GetType(),
                                        PropertyValue = val
                                    });
                                    Console.WriteLine(deProperty + ": " + val);
                                }
                            }

                            usersList.Add(user);
                            Console.WriteLine("==============");
                            Console.WriteLine();
                        }
                    }
                }

                var usersListJson = JsonConvert.SerializeObject(usersList);
                File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "users.json"), usersListJson);

                Console.WriteLine("Press any key to close");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine();
                Console.WriteLine("Press any key to close");
                Console.ReadLine();
            }
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
        public object PropertyName { get; set; }
        public Type PropertyType { get; set; }
        public object PropertyValue { get; set; }
    }
}