using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Kollaborator.web.Models
{
    public class ChatModel
    {
        [Key]
        public int messageID { get; set; }
        public string message { get; set; }
        
        public string sender { get; set; }
        public DateTime timestamp { get; set; }
        [ForeignKey("")]
        public int groupID { get; set; }
    }
}