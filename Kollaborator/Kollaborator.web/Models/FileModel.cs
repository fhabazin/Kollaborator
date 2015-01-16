using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Kollaborator.web.Models
{
    public class FileModel
    {
       
        [Key]
        public String path { get; set; }
        [ForeignKey("")]
        public int groupId { get; set; }
        public DateTime uploadDate { get; set; }
        public String FileType { get; set; }
        public String thumbnail { get; set; }

        public int fileId { get; set; }
        
    }
}