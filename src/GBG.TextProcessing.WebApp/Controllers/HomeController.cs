using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using GBG.Microservices.Messaging.Commands;
using GBG.TextProcessing.WebApp.Models;
using MassTransit;

namespace GBG.TextProcessing.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBusControl bus;

        public HomeController(IBusControl bus)
        {
            this.bus = bus;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(ProcessTextViewModel processText)
        {
            var message = new ProcessTextCommand
            {
                CreateDate = DateTime.UtcNow,
                Sender = processText.Sender,
                Text = processText.Text,
            };

            await bus.Publish(message);

            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}