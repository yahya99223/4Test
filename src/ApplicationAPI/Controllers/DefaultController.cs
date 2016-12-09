using System.Web.Http;
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
        public string Get()
        {
            var user = Core.Model.User.Create("Sameer");
            userService.Add(user);
            return "";
            /*var message = new StringBuilder();
            message.AppendLine(string.Format("BeginWebRequests :{0}", StaticInfo.BeginWebRequests));
            message.AppendLine(string.Format("EndWebRequests :{0}", StaticInfo.EndWebRequests));
            message.AppendLine("   ");
            message.AppendLine(Environment.NewLine);
            message.AppendLine(string.Format("StartedUnitOfWorks :{0}", StaticInfo.StartedUnitOfWorks));
            //message.AppendLine(string.Format("CommitedUnitOfWorks :{0}", StaticInfo.CommitedUnitOfWorks));
            message.AppendLine(string.Format("DisposedUnitOfWorks :{0}", StaticInfo.DisposedUnitOfWorks));
            message.AppendLine("   ");
            message.AppendLine(string.Format("Exception :{0}", StaticInfo.Exception));
            //message.AppendLine(string.Format("Users :{0}", StaticInfo.Users));
            return message.ToString();*/
        }
    }
}