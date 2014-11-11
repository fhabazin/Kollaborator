﻿using Kollaborator.web.Models;
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
            return View();
            
        }
    }
}