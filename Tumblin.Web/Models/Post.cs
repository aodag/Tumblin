using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tumblin.Web.Models
{
    public class Post
    {
        public int Id
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }
        
        public string Text
        {
            get;
            set;
        }
    }
}