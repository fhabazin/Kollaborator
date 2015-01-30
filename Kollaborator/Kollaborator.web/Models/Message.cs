using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kollaborator.web.Models
{
    public class Message
    {
        public string senderID { get; set; }
        public string message { get; set; }
        public string groupID { get; set; }
    }
}