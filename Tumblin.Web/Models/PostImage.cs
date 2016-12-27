using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tumblin.Web.Models
{
    public class PostImage
    {
        public int Id
        {
            get;
            set;
        }

        public byte[] Data
        {
            get;
            set;
        }

        public Post Post
        {
            get;
            set;
        }
    }
}