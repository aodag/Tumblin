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

        private IDbConnection Connection
        {
            get
            {
                return tx.Connection;
            }
        }

        public async Task<Models.Post> Get(int id)
        {
            return await Connection.QueryFirstAsync<Models.Post>("SELECT id Id, title Title, text Text FROM posts WHERE id = @id", new { id = id });
        }

        public async Task<IEnumerable<Models.Post>> Find()
        {
            return await Connection.QueryAsync<Models.Post>("SELECT id Id, title Title, text Text FROM posts");
        }

        public async Task<Models.Post> Add(Models.Post post)
        {
            var affected = await Connection.ExecuteAsync("INSERT INTO posts (title, text) VALUES (@Title, @Text)", post);
            var id = await Connection.ExecuteScalarAsync<int>("SELECT last_insert_id()");
            post.Id = id;
            return post;
        }

        public async Task Remove(int id)
        {
            await Connection.ExecuteAsync("DELETE FROM posts WHERE id = @Id", new { Id = id });
        }

        public async Task<Models.Post> Update(Models.Post post)
        {
            await Connection.ExecuteAsync("UPDATE posts SET title = @Title, text = @Text WHERE id = @Id", post);
            return post;
        }
    }
}