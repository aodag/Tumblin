using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Nancy;
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
        }

        protected override void ConfigureRequestContainer(IKernel container, NancyContext context)
        {
            container.Bind<IRepository<Models.Post>>().To<PostRepository>();
            container.Bind<System.Data.IDbTransaction>().ToMethod(x => Connect().BeginTransaction()).InSingletonScope();
        }
        System.Data.IDbConnection Connect()
        {
            var mySqlConnection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
            mySqlConnection.Open();
            return mySqlConnection;
        }
    }
}