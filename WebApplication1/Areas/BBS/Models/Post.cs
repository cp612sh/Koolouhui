using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Koo.Web.Areas.BBS.Models
{
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public Koo.Web.Models.ApplicationUser CreatedUser { get; set; }

        public IList<Post> RepliedPosts { get; set; }

        public int BrowseNum { get; set; }

    }


    

}