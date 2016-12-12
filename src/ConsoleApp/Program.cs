using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.Linq;
using System.Security.Claims;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //CheckWindowsAuthentication();

            SetupClaimIdentity();
            UseClaimsPrincipal();
            Console.ReadLine();

        }


        private static void SetupClaimIdentity()
        {
            Console.WriteLine("Who are you ?");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, Console.ReadLine()),
                new Claim(ClaimTypes.Email, "W.bitar@idscan.com"),
                new Claim(ClaimTypes.Role, "Developer"),
                new Claim("http://customClaims/Origin", "Syria"),
            };

            var id = new ClaimsIdentity(claims,"ConsoleApp");

            var principal = new ClaimsPrincipal(id);
            Thread.CurrentPrincipal = principal;
        }


        [ClaimsPrincipalPermission(SecurityAction.Demand, Operation = "DoWork", Resource = "SystemSettings")]
        public static void UseClaimsPrincipal()
        {
            var cp = ClaimsPrincipal.Current;
            Console.WriteLine($"is Authenticated: {cp.Identity.IsAuthenticated}");

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
