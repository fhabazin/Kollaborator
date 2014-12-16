using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Kollaborator.web.Models
{
    public class GroupModel
    {
        
        [Key]
        public int groupID { get; set; }
        public virtual ICollection<UserGroup> userGroups { get; set; }
        public String groupName { get; set; }
        [ForeignKey("")]
        public String creator { get; set; }
       
        public GroupModel()
        { }
       
       
    }

    
}