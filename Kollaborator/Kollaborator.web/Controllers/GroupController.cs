using Kollaborator.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Kollaborator.web.Controllers
{
    public class GroupController : Controller
    {
        // GET: Group
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var userName = WebSecurity.CurrentUserName;

                    var groupList = ctx.Users
                        .Where(p => p.UserName == userName)
                        .SelectMany(p => p.userGroups.Select(ug => ug.group)).
                        ToList();


                    return View(groupList);
                }
            }
            else
            {
                return View();
            }
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection formData)
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
                ViewBag.view = "group";
                return View(groupfiles);
            }
            
        }
        public PartialViewResult AddUserToGroup() 
        { 
            return PartialView("_AddUser");
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
                    FileType = MimeMapping.GetMimeMapping(file.FileName)
                };
                ctx.files.Add(fm);
                ctx.SaveChanges();
                return Content(Url.Content(@"~\Content\" + file.FileName));
            }
        }
        
    }


    }
