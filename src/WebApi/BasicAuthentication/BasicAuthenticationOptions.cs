using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;

namespace WebApi.BasicAuthentication
{
    public class BasicAuthenticationOptions:AuthenticationOptions
    {
        public string Realm { get; private set; }

        public BasicAuthenticationOptions(string realm) : base("Basic")
        {
            Realm = realm;
        }
    }

    public class BasicAuthenticationHandler:AuthenticationHandler<BasicAuthenticationOptions>
    {
        private string challenge;

        public BasicAuthenticationHandler()
        {
            challenge = "Basic realm=" + Options.Realm;
        }
        protected override Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            throw new NotImplementedException();
        }
    }
}