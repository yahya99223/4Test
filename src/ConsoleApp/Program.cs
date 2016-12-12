using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            CheckWindowsAuthentication();
            Console.ReadLine();

        }


        private static void CheckWindowsAuthentication()
        {
            var id = WindowsIdentity.GetCurrent();
            var account = new NTAccount(id.Name);
            var sid = account.Translate(typeof(SecurityIdentifier));
            Console.WriteLine($"User SID : {sid.Value}");

            foreach (var group in id.Groups.Translate(typeof(NTAccount)))
            {
                Console.WriteLine(group.Value);
            }

            Console.WriteLine("=================");
            var localAdmins = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);

            var principal = new WindowsPrincipal(id);
            Console.WriteLine($"Is Admin: {principal.IsInRole(localAdmins)}");
        }
    }
}
