using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using System.IO;

namespace Tumblin.Web
{
    public class ImageModule : NancyModule
    {
        public ImageModule(IRepository<Models.PostImage> repository): base("api/images")
        {
            Get["{id:int}", true] = async (_, ct) =>
            {
                var id = (int)_.id;
                var item = await repository.Get(id);

                return Response.FromStream(() => new MemoryStream(item.Data), "image/jpeg");
            };
        }
    }
}