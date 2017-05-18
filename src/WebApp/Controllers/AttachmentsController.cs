using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AttachmentsController : Controller
    {
        private UserAttachmentStore attachmentStore;

        public AttachmentsController()
        {
            attachmentStore = new UserAttachmentStore();
        }
        
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Upload()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<ActionResult> Upload(HttpPostedFileBase image)
        {
            try
            {
                if (image != null)
                {
                    var imageId = await attachmentStore.SaveImage(image.InputStream);
                    return RedirectToAction("Show", new {id = imageId});
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        
        public ActionResult Show(string id)
        {
            ViewBag.ImageUri = attachmentStore.UriFor(id);
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
