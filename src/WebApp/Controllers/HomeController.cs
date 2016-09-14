using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceTire;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private IFakeService fakeService;
        public HomeController()
        {
            fakeService = new FakeService("HomeController");
        }
        public ActionResult Index()
        {
            fakeService.DoGoodWork();
            return View();
        }

        public ActionResult About()
        {
            fakeService.DoBadWork();

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            fakeService.DoError();

            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}