﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.ModelBinding;

namespace Tumblin.Web
{
    public class PostsModule: NancyModule
    {
        public PostsModule(System.Data.IDbTransaction tx, IRepository<Models.Post> repository, IRepository<Models.PostImage> images) :base("/api/posts")
        {
            After += ctx => tx.Commit();
            Get["", true] = async (_, ct) => Response.AsJson(await repository.Find());
            Post["", true] = async (_, ct) => {
                var post = this.Bind<Models.Post>();
                post = await repository.Add(post);
                var files = Request.Files.ToArray();
                if (files.Length > 0)
                {
                    var file = files[0].Value;
                    var size = (int)file.Length;
                    var buffer = new byte[size];
                    await file.ReadAsync(buffer, 0, size);
                    var image = new Models.PostImage() { Post = post, Data = buffer };
                    await images.Add(image);
                }
                return Response.AsJson(post);
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