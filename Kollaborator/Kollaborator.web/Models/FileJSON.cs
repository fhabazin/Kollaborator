using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kollaborator.web.Models
{
    public class FileJSON
    {
        public int id { get; set; }
        public string fullPath { get; set; }
        public string thumbnailPath { get; set; }
        public string mime { get; set; }  

        
    }
}