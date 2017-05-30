using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GBG.Microservices.Messaging.Commands;
using GBG.TextProcessing.WebApp.Models;
using NServiceBus;

namespace GBG.TextProcessing.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEndpointInstance endpoint;

        public HomeController(IEndpointInstance endpoint)
        {
            this.endpoint = endpoint;
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
            await endpoint.Send("TextProcessing.ConsoleApp", message).ConfigureAwait(false);

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