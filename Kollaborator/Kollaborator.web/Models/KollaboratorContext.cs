using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Kollaborator.web.Models
{
    public class KollaboratorContext : DbContext
    {
        public DbSet<FileModel> files { get; set; }
    }
}