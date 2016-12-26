using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.ModelBinding;

namespace Tumblin.Web
{
    public class TumblinModule: NancyModule
    {
        public TumblinModule(System.Data.IDbTransaction tx, IRepository<Models.Post> repository)
        {
            After += ctx => tx.Commit();
            Get["/"] = _ => "Hello";
            Get["/api/posts", true] = async (_, ct) => Response.AsJson(await repository.Find());
            Post["/api/posts", true] = async (_, ct) => {
                var post = this.Bind<Models.Post>();
                return Response.AsJson(await repository.Add(post));
            };
            Put["/api/posts/{Id}", true] = async (_, ct) =>
            {
                var post = this.Bind<Models.Post>();
                return Response.AsJson(await repository.Update(post));
            };
            Delete["/api/posts/{Id}", true] = async (_, ct) =>
            {
                await repository.Remove(_.Id);
                return Response.AsJson(true);
            };
        }
    }
}