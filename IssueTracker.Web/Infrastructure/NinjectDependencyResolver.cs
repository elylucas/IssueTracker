using System.Web.Http.Dependencies;
using Ninject;

namespace IssueTracker.Web.Infrastructure
{
    public class NinjectWebApiDependencyResolver : NinjectDependencyScope, IDependencyResolver
    {
        private IKernel _kernel;

        public NinjectWebApiDependencyResolver(IKernel kernel)
            : base(kernel)
        {
            _kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyScope(_kernel.BeginBlock());
        }
    }
}