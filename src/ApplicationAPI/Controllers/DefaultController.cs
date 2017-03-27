using System.Threading.Tasks;
using System.Web.Http;
using Core;
using Core.Services;
using IoC;

namespace ApplicationAPI.Controllers
{
    public class DefaultController : ApiController
    {
        private readonly IServiceResolver serviceResolver;

        public DefaultController()
        {
            this.serviceResolver = ServiceResolverFactory.GetServiceResolver();
        }

        [HttpGet]
        [Route("api/Print")]
        public IHttpActionResult Print()
        {
            var result = new
            {
                StaticInfo.BeginWebRequests,
                StaticInfo.EndWebRequests,
                StaticInfo.StartedUnitOfWorks,
                StaticInfo.CommitedUnitOfWorks,
                StaticInfo.DisposedUnitOfWorks,
                StaticInfo.Users,
                StaticInfo.Exception
            };
            return Ok(result);
        }

        [HttpGet]
        [Route("api/Add/Sync")]
        public IHttpActionResult AddSync()
        {
            StaticInfo.BeginWebRequests += 1;
            var user = Core.Model.User.Create("Sameer");
            var userService = serviceResolver.Resolve<IUserService>();
            userService.Add(user);

            StaticInfo.EndWebRequests += 1;
            var result = new
            {
                StaticInfo.BeginWebRequests,
                StaticInfo.EndWebRequests,
                StaticInfo.StartedUnitOfWorks,
                StaticInfo.CommitedUnitOfWorks,
                StaticInfo.DisposedUnitOfWorks,
                StaticInfo.Users,
                StaticInfo.Exception
            };
            return Ok(result);
        }

        [HttpGet]
        [Route("api/Add/Async")]
        public async Task<IHttpActionResult> AddAsync()
        {
            StaticInfo.BeginWebRequests += 1;

            var user = Core.Model.User.Create("Sameer");
            var userService = serviceResolver.Resolve<IUserService>();
            await userService.AddAsync(user);

            StaticInfo.EndWebRequests += 1;

            var result = new
            {
                StaticInfo.BeginWebRequests,
                StaticInfo.EndWebRequests,
                StaticInfo.StartedUnitOfWorks,
                StaticInfo.CommitedUnitOfWorks,
                StaticInfo.DisposedUnitOfWorks,
                StaticInfo.Users,
                StaticInfo.Exception
            };
            return Ok(result);
        }
    }
}