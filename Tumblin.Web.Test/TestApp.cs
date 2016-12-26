using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Configuration;

namespace Tumblin.Web.Test
{
    [TestFixture]
    public class TestApp
    {
        [Test]
        public void TestIt()
        {
            var bootstrapper = new TumblinBootstrapper();
            var browser = new Nancy.Testing.Browser(bootstrapper);
            var response = browser.Get("/api/posts");
        }

        [Test]
        public void TestFailure()
        {
            var connectionSettings = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            Assert.NotNull(connectionSettings);
        }
    }
}
