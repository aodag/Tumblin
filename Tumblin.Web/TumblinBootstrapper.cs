using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy.Bootstrappers.Ninject;
using Ninject;

namespace Tumblin.Web
{
    public class TumblinBootstrapper: NinjectNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(IKernel existingContainer)
        {
            existingContainer.Bind<PostRepository>().To<PostRepository>();
        }
    }
}