using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Data;
using Dapper;

namespace Tumblin.Web
{

    public class PostRepository
    {
        private IDbTransaction tx;

        public PostRepository(IDbTransaction tx)
        {
            this.tx = tx;
        }

        private IDbConnection Connect()
        {
            return tx.Connection;
        }

        public async Task<Models.Post> Get(int id)
        {
            using (var conn = Connect())
            {
                return await conn.QueryFirstAsync<Models.Post>("SELECT id Id, title Title, text Text FROM posts WHERE id = @id", new { id = id });
            }
        }

        public async Task<IEnumerable<Models.Post>> Find()
        {
            using (var conn = Connect())
            {
                return await conn.QueryAsync<Models.Post>("SELECT id Id, title Title, text Text FROM posts");
            }
        }

        public async Task<Models.Post> Add(Models.Post post)
        {
            using (var conn = Connect())
            {
                var affected = await conn.ExecuteAsync("INSERT INTO posts (title, text) VALUES (@Title, @Text)", post);
                var id = await conn.ExecuteScalarAsync<int>("SELECT last_insert_id()");
                post.Id = id;
                tx.Commit();
            }
            return post;
        }

        public void Remove(Models.Post post)
        {

        }
    }
}