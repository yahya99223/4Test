using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.UI.WebControls;
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
        public IHttpActionResult Get()
        {
            return Ok(StaticInfo.UnitOfWorks);
        }
    }
}