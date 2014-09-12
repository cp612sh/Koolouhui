using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Koo.Web.Models
{

    public class CartItem
    {
        public int ProjectId { get; set; }

        public string Amount { get; set; }

        public Project Project { get; set; }
    }
}