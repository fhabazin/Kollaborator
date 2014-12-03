using Kollaborator.web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

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
        public ActionResult FileUpload(int groupID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var file = Request.Files["Filedata"];

                string savePath = Server.MapPath(@"~\Content\" + file.FileName);
                file.SaveAs(savePath);
                FileModel fm = new FileModel()
                {
                    path = savePath,
                    groupId = groupID,
                    uploadDate = DateTime.Now,
                    FileType = file.ContentType
                };
                ctx.files.Add(fm);
                ctx.SaveChanges();
                return Content(Url.Content(@"~\Content\" + file.FileName));
            }
        }




        public ActionResult Upload()
        {
            using (var ctx = new ApplicationDbContext())
            {

                var query = ctx.files.Where(p => p.groupId == 11).
                            OrderBy(p => p.uploadDate).Select(p => p.path).ToList();
                List<String> paths = new List<String>();
                foreach (String path in query)
                {
                    paths.Add(Url.Content(@"~\Content\" + Path.GetFileName(path)));
                }
                return View(paths);
            }


        }


    }
}