using Kollaborator.web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
                ctx.Groups.Add(group);
                ctx.userGroups.Add(usergroup);
                ctx.SaveChanges();
                foreach (var person in users)
                {
                    var  sth = ctx.Users.FirstOrDefault(p=> p.UserName.Equals(person.UserName));
                    if (sth!=null)
                    {
                        usergroup = new UserGroup
                        {
                            UserID =sth.Id,
                            
                            groupID= group.groupID,
                            
                        };
                        ctx.userGroups.Add(usergroup);
                    }
                   
                    
                }
                try {
                    // Your code...
                    // Could also be before try if you know the exception occurs in SaveChanges

                    ctx.SaveChanges();
                }
                catch (DbEntityValidationException e){
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    
                }

              
             }
             
             return RedirectToAction("Index");
        }
       

        public PartialViewResult Group(int groupID)
        {
            using(var ctx = new ApplicationDbContext()){
                var files = ctx.files.Where(p => p.groupId == groupID).ToList();
                var messages = ctx.chat.Where(p => p.groupID == groupID).ToList();
                var groupdata = new Tuple<GroupModel,List<FileModel>,List<ChatModel>>(ctx.Groups.Where(p => p.groupID ==groupID).FirstOrDefault(), files, messages);
                ViewBag.view = "group";
                return PartialView("_Group",groupdata);
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

        public PartialViewResult Management(int groupID){
            using(var ctx = new ApplicationDbContext()){
                var users = ctx.userGroups
                    .Where(p => p.groupID == groupID)
                    .Select(p => p.user).ToList();
                return PartialView("_Management",users);
            }
        }
        public JsonResult CountryList()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var countries = ctx;
                return Json(countries, JsonRequestBehavior.AllowGet);
            }
        }

        public void send(ChatModel message)
        {

        }
    }


    }
