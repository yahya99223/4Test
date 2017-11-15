using System.Threading;
using System.Web.Http;
using IdentityManager;
using IdentityManager.AspNetIdentity;
using IdentityManager.Configuration;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.BuilderProperties;
using Microsoft.Owin.Cors;
using Owin;

namespace WebApp
{
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            OnAppDisposing(app);
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);

            var factory = new IdentityManagerServiceFactory();
            factory.IdentityManagerService = new Registration<IIdentityManagerService>(Create());

            app.UseCors(CorsOptions.AllowAll);
            app.UseIdentityManager(new IdentityManagerOptions {Factory = factory});
            //app.UseWebApi(config);
        }




        private static void OnAppDisposing(IAppBuilder app)
        {
            var properties = new AppProperties(app.Properties);
            CancellationToken token = properties.OnAppDisposing;
            if (token != CancellationToken.None)
            {
                token.Register(() =>
                {
                    //No need to pass folder because we should've been already created the instance
                });
            }
        }



        private IIdentityManagerService Create()
        {
            var context = new IdentityDbContext(@"IdentityManagerDB");

            var userStore = new UserStore<IdentityUser>(context);
            var userManager = new UserManager<IdentityUser>(userStore);

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var managerService =
                new AspNetIdentityManagerService<IdentityUser, string, IdentityRole, string>
                    (userManager, roleManager);

            return managerService;
        }
    }
}