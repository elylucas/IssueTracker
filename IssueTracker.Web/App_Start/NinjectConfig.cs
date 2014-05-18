using System.Web.Http;
using IssueTracker.Data;
using IssueTracker.Data.Contracts;
using IssueTracker.Repository;
using IssueTracker.Repository.Contracts;
using IssueTracker.Web.Infrastructure;
using Ninject;
using Ninject.Web.Common;

namespace IssueTracker.Web
{
    public class NinjectConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            // Set up Ninject dependency resolver.

            IKernel container = new StandardKernel();
         
            AddBindings(container);

            //Tell Web API to use Ninject
            config.DependencyResolver = new NinjectWebApiDependencyResolver(container);
        }

        private static void AddBindings(IKernel container)
        {
           container.Bind<IPersonRepository>().To<PersonRepository>().InRequestScope();
           container.Bind<IIssueRepository>().To<IssueRepository>().InRequestScope();
           container.Bind<IContext>().To<IssueTrackerContext>().InRequestScope();
        }

    }
}