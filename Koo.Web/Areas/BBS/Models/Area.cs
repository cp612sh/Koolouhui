using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Koo.Web.Areas.BBS.Models
{
    public class Area
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<Koo.Web.Models.ApplicationUser> Moderators { get; set; }


    }
}