using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Kollaborator.web.Models
{
    public class UserGroup
    {

        [Key, Column(Order = 0)]
        public String UserID { get; set; }

        [Key, Column(Order = 1)]
        public int groupID { get; set; }
        public virtual ApplicationUser user { get; set; }
        public virtual GroupModel group { get; set; }
    }
}