using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Tumblin.Web.Models;
using System.Data;
using Dapper;

namespace Tumblin.Web
{
    public class ImageRepository : IRepository<PostImage>
    {
        private IDbTransaction tx;

        public ImageRepository(IDbTransaction tx)
        {
            this.tx = tx;
        }

        private IDbConnection Connection
        {
            get
            {
                return tx.Connection;
            }
        }

        public async Task<PostImage> Add(PostImage model)
        {
            var affected = await Connection.ExecuteAsync(
                "INSERT INTO images (post_id, data) VALUES (@PostId, @Data)",
                new { PostId = model.Post.Id, Data = model.Data });
            model.Id = await Connection.ExecuteScalarAsync<int>("SELECT last_insert_id()");
            model.Post.ImageId = model.Id;
            return model;
        }

        public Task<IEnumerable<PostImage>> Find()
        {
            throw new NotImplementedException();
        }

        public async Task<PostImage> Get(int id)
        {
            return await Connection.QueryFirstAsync<Models.PostImage>("SELECT id Id, data Data FROM images WHERE id = @Id", new { Id = id });
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PostImage> Update(PostImage model)
        {
            throw new NotImplementedException();
        }
    }
}