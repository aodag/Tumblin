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
        private Func<IDbConnection> connectionFactory;

        public PostRepository(Func<IDbConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        private IDbConnection Connect()
        {
            var conn = connectionFactory.Invoke();
            conn.Open();
            return conn;
        }

        public async Task<Models.Post> Get(int id)
        {
            using (var conn = Connect())
            {
                return await conn.QueryFirstAsync<Models.Post>("SELECT id Id, title Title, text Text FROM posts WHERE id = @id", new { id = id });
            }
        }

        public async Task<IEnumerable <Models.Post>> Find()
        {
            using (var conn = Connect())
            {
                return await conn.QueryAsync<Models.Post>("SELECT id Id, title Title, text Text FROM posts");
            }
        }

        public async void Add(Models.Post post)
        {
            using (var conn = Connect())
            {
                var affected = await conn.ExecuteAsync("INSERT INTO posts (title, text) VALUES (@Title, @Text)", post);
            }
        }

        public void Remove(Models.Post post)
        {

        }
    }
}