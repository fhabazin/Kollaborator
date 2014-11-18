using Kollaborator.web.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
        public ActionResult CreateGroup()
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
            using (var ctx = new KollaboratorContext())
            {
                var file = Request.Files["Filedata"];

                string savePath = Server.MapPath(@"~\Content\" + file.FileName);
                file.SaveAs(savePath);
                FileModel fm = new FileModel()
                {
                    path = savePath,
                    groupId = 11,
                    uploadDate = DateTime.Now
                };
                ctx.files.Add(fm);
                ctx.SaveChanges();
                return Content(Url.Content(@"~\Content\" + file.FileName));
            }
        }




        public ActionResult Upload()
        {
            using (var ctx = new KollaboratorContext())
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

        public void SaveGroup(FormCollection formData)
        {
            Console.Write("Save gropu....");
            using (var gctx = new GroupModelContext())
            {
                // GroupModel model = new GroupModel();
                GroupModel ime = new GroupModel(formData["groupName"], 1, 1);
                gctx.GroupModel.Add(ime);
                gctx.SaveChanges();

                //    var query = gctx.files.Where(p => p.groupId == 11).
                //              OrderBy(p => p.uploadDate).Select(p => p.path).ToList();

                //  return View(paths);
            }




        }
    }
}