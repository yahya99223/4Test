using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.UI.WebControls;
using Core;
using Core.DataAccess;
using Core.Model;
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
            userService.Add(Core.Model.User.Create("Sameer"));
            var message = new StringBuilder();
            message.AppendLine(string.Format("BeginWebRequests :{0}", StaticInfo.BeginWebRequests));
            message.AppendLine(string.Format("EndWebRequests :{0}", StaticInfo.EndWebRequests));
            message.AppendLine("");
            message.AppendLine(string.Format("StartedUnitOfWorks :{0}", StaticInfo.StartedUnitOfWorks));
            message.AppendLine(string.Format("CommitedUnitOfWorks :{0}", StaticInfo.CommitedUnitOfWorks));
            message.AppendLine(string.Format("DisposedUnitOfWorks :{0}", StaticInfo.DisposedUnitOfWorks));
            message.AppendLine("");
            message.AppendLine(string.Format("Users :{0}", StaticInfo.Users));
            return message.ToString();
        }
    }
}