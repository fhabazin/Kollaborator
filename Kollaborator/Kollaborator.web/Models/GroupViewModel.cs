using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kollaborator.web.Models
{
    public class GroupViewModel
    {
        public GroupModel group;
        public List<FileModel> files;
        public List<String> users;
        public List<ChatModel> messages;
        public GroupViewModel(int groupID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                 group = ctx.Groups.Where(p => p.groupID == groupID).FirstOrDefault();
                 users = ctx.userGroups.Where(p => p.groupID == groupID).Select(p => p.user.UserName).ToList();
                 files = ctx.files.Where(p => p.groupId == groupID).ToList();
                 messages = ctx.chat.Where(p => p.groupID == groupID).ToList();
            }
        }
    }
}