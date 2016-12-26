using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Nancy.Bootstrappers.Ninject;
using Ninject;

namespace Tumblin.Web
{
    public class TumblinBootstrapper: NinjectNancyBootstrapper
    {
        private string connectionString;

        protected override void ConfigureApplicationContainer(IKernel existingContainer)
        {
            connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            existingContainer.Bind<Func<System.Data.IDbConnection>>().ToMethod(x => Connect);
            existingContainer.Bind<PostRepository>().To<PostRepository>();
        }

        System.Data.IDbConnection Connect()
        {
            return new MySql.Data.MySqlClient.MySqlConnection(connectionString);
        }
    }
}