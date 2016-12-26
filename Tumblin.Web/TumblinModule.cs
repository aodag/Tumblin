using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace Tumblin.Web
{
    public class TumblinModule: NancyModule
    {
        public TumblinModule()
        {
            Get["/"] = _ => "Hello";
        }
    }
}