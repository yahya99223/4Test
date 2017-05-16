using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class TasksController : Controller
    {
        private UserTaskStore store;

        public TasksController()
        {
            store = new UserTaskStore();
        }

        // GET: Tasks
        public ActionResult Index()
        {
            var tasks = store.GetAll();
            return View(tasks);
        }

        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserTask task)
        {
            task.id = Guid.NewGuid();
            task.CreateDate = DateTime.UtcNow;
            
            await store.Add(task);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(Guid id)
        {
            var task = await store.GetById(id);
            return View(task);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(Guid id)
        {
            await store.Delete(id);
            return RedirectToAction("Index");
        }
    }
}