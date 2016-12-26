using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tumblin.Web
{
    public class PostRepository
    {
        public Models.Post Get(int id)
        {
            return new Models.Post() { Id = id };
        }

        public IEnumerable <Models.Post> Find()
        {
            return new Models.Post[] { };
        }

        public void Add(Models.Post post)
        {

        }

        public void Remove(Models.Post post)
        {

        }
    }
}