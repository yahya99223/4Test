using System;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Core;
using Core.DataAccess;
using Core.Services;


namespace ApplicationAPI.Controllers
{
    public class DefaultController : ApiController
    {
        private readonly IUserService userService;


        public DefaultController(IUserService userService)
        {
            this.userService = userService;
        }


        [Route("api/test")]
        public IHttpActionResult Add()
        {

            var user = Core.Model.User.Create("Sameer");
            userService.Add(user);

            var result = new
            {
                StaticInfo.BeginWebRequests,
                EndWebRequests = StaticInfo.EndWebRequests + 1,
                StaticInfo.StartedUnitOfWorks,
                StaticInfo.DisposedUnitOfWorks,
                StaticInfo.Users,
                StaticInfo.Exception,
            };
            return Ok(result);
        }

        [Route("api/test/async")]
        public async Task<IHttpActionResult> AddAsync()
        {

            var user = Core.Model.User.Create("Sameer");
            await userService.AddAsync(user);

            var result = new
            {
                StaticInfo.BeginWebRequests,
                EndWebRequests = StaticInfo.EndWebRequests + 1,
                StaticInfo.StartedUnitOfWorks,
                StaticInfo.DisposedUnitOfWorks,
                StaticInfo.Users,
                StaticInfo.Exception,
            };

            return Ok(result);
        }
    }
}