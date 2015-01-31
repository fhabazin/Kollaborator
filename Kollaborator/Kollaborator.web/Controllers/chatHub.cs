using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using Kollaborator.web.Models;
using WebMatrix.WebData;
using System.Web.Mvc;
using System.Text;
using System.IO;
using System.Web.Routing;
using Westwind.Web.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNet.SignalR.Hubs;
using System.Text.RegularExpressions;

namespace Kollaborator.web.Controllers
{
    public class chatHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public Task JoinRoom(string roomName)
        {
            
            return Groups.Add(Context.ConnectionId, roomName);
        }
        public void Subscribe(int groupID){

            using (var ctx = new ApplicationDbContext())
            {

                var usersInGroup = ctx.userGroups.Where(p => p.groupID == groupID).Select(p => p.user.UserName).ToList();

                if (usersInGroup.Contains(WebSecurity.CurrentUserName))
                {
                    var groupName = ctx.Groups.Where(p => p.groupID == groupID).FirstOrDefault().groupName;
                    JoinRoom(groupName);
                }
            }
        }
        public Task LeaveRoom(string roomName)
        {
            return Groups.Remove(Context.ConnectionId, roomName);
            
        }

        public void Send(int groupID, string message)
        {
            // Call the addMessage method on all clients            
            using (var ctx = new ApplicationDbContext())
            {
                var model = new ChatModel();
                model.message = MakeLink(message);
                model.groupID = groupID;
                model.sender = WebSecurity.CurrentUserName;
                model.timestamp = DateTime.Now; 
                var groupName = ctx.Groups.Where(p => p.groupID == model.groupID).FirstOrDefault().groupName;
                var groupMessage = JsonConvert.SerializeObject(model);
                string methodToCall = "broadcastMessage";
                IClientProxy proxy = Clients.Group(groupName);
                proxy.Invoke(methodToCall, groupName, groupMessage);
               
                ctx.chat.Add(model);
                ctx.SaveChanges();
            }
        }


        protected string MakeLink(string txt)
        {
            Regex regx = new Regex("http://([\\w+?\\.\\w+])+([a-zA-Z0-9\\~\\!\\@\\#\\$\\%\\^\\&amp;\\*\\(\\)_\\-\\=\\+\\\\\\/\\?\\.\\:\\;\\'\\,]*)?", RegexOptions.IgnoreCase);
            MatchCollection mactches = regx.Matches(txt);
            foreach (Match match in mactches)
            {
                txt = txt.Replace(match.Value, "<a href='" + match.Value + "'>" + match.Value + "</a>");
                
            }
            return txt;
        }

        

    }
}