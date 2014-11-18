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
        private string p;
        private string r;
        private string s;
        [Key]
        public int groupID { get; set; }

        public String groupName { get; set; }
        [ForeignKey("")]
        public int creatorID { get; set; }
        public GroupModel(string p, int r, int s)
        {
            
            this.groupID = r;
            this.groupName = p;
            this.creatorID = s;
        }
       
    }

    public class GroupModelContext:DbContext {
        public DbSet<GroupModel> GroupModel { get; set; }
        public GroupModelContext() 
        : base("DefaultConnection") 
    { 
    } 
        }
}