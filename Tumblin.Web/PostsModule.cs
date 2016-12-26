using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.ModelBinding;

namespace Tumblin.Web
{
    public class PostsModule: NancyModule
    {
        public PostsModule(System.Data.IDbTransaction tx, IRepository<Models.Post> repository) :base("/api/posts")
        {
            After += ctx => tx.Commit();
            Get["", true] = async (_, ct) => Response.AsJson(await repository.Find());
            Post["", true] = async (_, ct) => {
                var post = this.Bind<Models.Post>();
                return Response.AsJson(await repository.Add(post));
            };
            Put["{Id}", true] = async (_, ct) =>
            {
                var post = this.Bind<Models.Post>();
                return Response.AsJson(await repository.Update(post));
            };
            Delete["{Id}", true] = async (_, ct) =>
            {
                await repository.Remove(_.Id);
                return Response.AsJson(true);
            };
        }
    }
}