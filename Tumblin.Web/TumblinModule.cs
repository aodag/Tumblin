using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace Tumblin.Web
{
    public class TumblinModule: NancyModule
    {
        public TumblinModule(PostRepository repository)
        {
            Get["/"] = _ => "Hello";
            Get["/api/posts"] = _ => Response.AsJson(repository.Find());
        }
    }
}