using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;
using GBG.Microservices.Messaging.Commands;
using GBG.TextProcessing.WebApp.Models;
using MassTransit;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace GBG.TextProcessing.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly string busConnectionString;
        private readonly string topicPath;
        private readonly IBusControl bus;

        public HomeController(IBusControl bus)
        {
            this.bus = bus;
            busConnectionString = ConfigurationManager.AppSettings["Microsoft.ServiceBus.ConnectionString"];
            topicPath = "ProcessTextCommand";
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

        [HttpPost]
        public async Task<ActionResult> Native(ProcessTextViewModel processText)
        {
            var busManager = NamespaceManager.CreateFromConnectionString(busConnectionString);

            if (!await busManager.TopicExistsAsync(topicPath))
            {
                await busManager.CreateTopicAsync(topicPath);
            }

            var messagingFactory = MessagingFactory.CreateFromConnectionString(busConnectionString);
            var topicClient = messagingFactory.CreateTopicClient(topicPath);

            var message = new ProcessTextCommand
            {
                CreateDate = DateTime.UtcNow,
                Sender = processText.Sender,
                Text = processText.Text,
            };

            await topicClient.SendAsync(new BrokeredMessage(message));
            await messagingFactory.CloseAsync();

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