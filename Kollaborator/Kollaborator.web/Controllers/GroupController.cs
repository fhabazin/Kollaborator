using Kollaborator.web.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
        public PartialViewResult Create()
        {
            return PartialView("_Create");
        }
        [HttpPost]




        public ActionResult Create(GroupModel group, IEnumerable<ApplicationUser> users)
        {
             using (var ctx = new ApplicationDbContext())
            {
                var user = ctx.Users.Where(p => p.UserName == WebSecurity.CurrentUserName).FirstOrDefault();
                group.creator = user.UserName;
                var usergroup = new UserGroup
                {
                    user = user,
                    group = group
                };
                ctx.SaveChanges();
                foreach (var person in users)
                {
                    var  sth = ctx.Users.Any(p=> p.UserName.Equals(person.UserName));
                    if (sth)
                    {
                        usergroup = new UserGroup
                        {
                            user = person,
                            group = group
                        };
                    }
                    ctx.SaveChanges();
                }
                
             }
             
             return RedirectToAction("Index");
        }
       

        public PartialViewResult Group(int groupID)
        {
            using(var ctx = new ApplicationDbContext()){
                var files = ctx.files.Where(p => p.groupId == groupID).ToList();

                Tuple<GroupModel,List<FileModel>> groupfiles = new Tuple<GroupModel,List<FileModel>>(ctx.Groups.Where(p => p.groupID ==groupID).FirstOrDefault(), files);
                ViewBag.view = "group";
                return PartialView("_Group",groupfiles);
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
                var thumbnailPath = "";
                if (MimeMapping.GetMimeMapping(file.FileName).Contains("image/"))
                {
                    var image = new Bitmap(savePath);
                    var thumbnail = image.GetThumbnailImage(100, 100, null, IntPtr.Zero);
                    thumbnailPath = Server.MapPath(@"~\Content\" 
                        + Path.GetFileNameWithoutExtension(file.FileName) 
                        + "_thumbnail" + Path.GetExtension(file.FileName));
                    thumbnail.Save(thumbnailPath);
                    
                }
                FileModel fm = new FileModel()
                    {
                        path = savePath,
                        groupId = groupID,
                        uploadDate = DateTime.Now,
                        FileType = MimeMapping.GetMimeMapping(file.FileName),
                        thumbnail = thumbnailPath
                    };
                ctx.files.Add(fm);
                ctx.SaveChanges();
                return Content(Url.Content(@"~\Content\" + file.FileName));
            }
        }
        
    }


    }
