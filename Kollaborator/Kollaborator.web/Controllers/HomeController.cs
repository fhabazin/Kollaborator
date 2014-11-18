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
            if (Request.IsAuthenticated)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var userName = WebSecurity.CurrentUserName;
                    var groupList = ctx.Users
                        .Where(p => p.UserName == userName)
                        .SelectMany(p=> p.userGroups.Select(ug=>ug.group)).
                        ToList();

                  
                    return View(groupList);
                }
            }
            else
            {
                return View();
            }
           
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
            using (var ctx = new ApplicationDbContext())
            {
                var file = Request.Files["Filedata"];
                
                string savePath = Server.MapPath(@"~\Content\" + file.FileName);
                file.SaveAs(savePath);
                FileModel fm = new FileModel()
                {
                    path = savePath,
                    groupId = 11,
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

        public ActionResult SaveGroup(FormCollection formData)
        {
            Console.Write("Save gropu....");
            using (var ctx = new ApplicationDbContext())
            {
                 GroupModel group = new GroupModel{
                    groupName = formData["groupName"]
                };
                var user = ctx.Users.Where(p => p.UserName == WebSecurity.CurrentUserName).FirstOrDefault();
                var usergroup = new UserGroup
                {
                    user = user,
                    group = group
                };
                ctx.userGroups.Add(usergroup);
                
                
                
                ctx.SaveChanges();
                return View("Index");
            }
         }

        public ActionResult Group(int groupID)
        {
            using(var ctx = new ApplicationDbContext()){
                var files = ctx.files.Where(p => p.groupId == groupID).ToList();
                Tuple<GroupModel,List<FileModel>> groupfiles = new Tuple<GroupModel,List<FileModel>>(ctx.Groups.Where(p => p.groupID ==groupID).FirstOrDefault(), files);
                return View(groupfiles);
            }
            
        }
    }
}