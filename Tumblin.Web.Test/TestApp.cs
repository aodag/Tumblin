using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NUnit.Framework;
using System.Configuration;
using Nancy.Testing;

namespace Tumblin.Web.Test
{
    [TestFixture]
    public class TestApp
    {
        [OneTimeSetUp]
        public void CreateTables()
        {
            var connectionSettings = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            var assemblyUnderTest = System.Reflection.Assembly.GetAssembly(typeof(TumblinBootstrapper));
            using (var conn = new MySql.Data.MySqlClient.MySqlConnection(connectionSettings.ConnectionString))
            {
                conn.Open();
                foreach (var resourceName in new string[] { "Tumblin.Web.sql.dropall.sql", "Tumblin.Web.sql.posts.sql", "Tumblin.Web.sql.images.sql" })
                {
                    using (var sql = new StreamReader(assemblyUnderTest.GetManifestResourceStream(resourceName)))
                    {
                        var stmt = sql.ReadToEnd();
                            var command = conn.CreateCommand();
                            command.CommandText = stmt;
                            command.ExecuteNonQuery();
                    }

                }
            }
        }

        private Nancy.Testing.Browser CreateApp()
        {
            var bootstrapper = new TumblinBootstrapper();
            var browser = new Browser(bootstrapper);
            return browser;
        }

        [Test]
        public void TestIt()
        {
            var browser = CreateApp();
            var response = browser.Get("/api/posts");
            Assert.AreEqual(Nancy.HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void TestCreate()
        {
            var browser = CreateApp();
            var response = browser.Post("/api/posts", context =>
            {
                context.JsonBody(new { Title = "Testing", Text = "testing text" });
            });
            Assert.AreEqual(Nancy.HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("application/json; charset=utf-8", response.Body.ContentType);
            var post = response.Body.DeserializeJson<Models.Post>();
            Assert.AreEqual("Testing", post.Title);
            Assert.AreEqual("testing text", post.Text);
        }

        [Test]
        public void TestUpdate()
        {
            var browser = CreateApp();
            var response = browser.Post("/api/posts", context =>
            {
                context.JsonBody(new { Title = "Testing", Text = "testing text" });
            });
            var post = response.Body.DeserializeJson<Models.Post>();
            var updatedResponse = browser.Put(string.Format("/api/posts/{0}", post.Id), context =>
            {
                context.JsonBody(new { Title = "Updated Testing", Text = "updated testing text" });

            });
            var updated = updatedResponse.Body.DeserializeJson<Models.Post>();
            Assert.AreEqual("Updated Testing", updated.Title);
            Assert.AreEqual("updated testing text", updated.Text);
        }

        [Test]
        public void TestFailure()
        {
            var connectionSettings = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            Assert.NotNull(connectionSettings);
        }
    }
}
