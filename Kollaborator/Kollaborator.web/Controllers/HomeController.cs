using Kollaborator.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kollaborator.web.Controllers
{
    public class HomeController : Controller
    {
        private IFileStore _fileStore = new DiskFileStore();
        public ActionResult Index()
        {
            return View();
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
        public ActionResult FileUpload()
        {
            var file = Request.Files["Filedata"];

            string savePath = Server.MapPath(@"~\Content\" + file.FileName);
            file.SaveAs(savePath);
            return Content(Url.Content(@"~\Content\" + file.FileName));
            
        }


        public Guid AsyncUpload()
        {
            return _fileStore.SaveUploadedFile(Request.Files[0]);
        }
        
        public ActionResult Upload()
        {
            return View();
            
        }
    }
}