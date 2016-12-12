using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp
{
    public class AuthorizationManager : ClaimsAuthorizationManager
    {
        public override bool CheckAccess(AuthorizationContext context)
        {
            var action = context.Action.FirstOrDefault();
            var resource = context.Resource.FirstOrDefault();

            if (action != null && action.Value == "DoWork" && resource != null && resource.Value == "SystemSettings")
            {
                return context.Principal.HasClaim(ClaimTypes.Name, "WahidBitar");
            }
            return false;
        }
    }
}
