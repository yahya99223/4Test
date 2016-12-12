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
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserService userService;


        public DefaultController(IUnitOfWork unitOfWork, IUserService userService)
        {
            this.unitOfWork = unitOfWork;
            this.userService = userService;
        }


        [Route("api/test")]
        public async Task<IHttpActionResult> Get()
        {
            await Task.Run(() =>
                           {
                               var user = Core.Model.User.Create("Sameer");
                               userService.Add(user);
                           });
            //return "";
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