using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kollaborator.web.Controllers
{
    public class HomeController : Controller
    {
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
            return View();
        }


        [HttpPost]
        public Guid FileUpload(HttpPostedFileBase photo)
        {
            string path = @"D:\Temp\";

            if (photo != null)
                photo.SaveAs(path + photo.FileName);

            return new Guid();
        }
    }
}