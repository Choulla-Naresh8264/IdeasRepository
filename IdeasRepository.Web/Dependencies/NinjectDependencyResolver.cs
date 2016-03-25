using IdeasRepository.BL.Interfaces;
using IdeasRepository.BL.Providers;
using IdeasRepository.DAL.Contexts;
using IdeasRepository.DAL.Managers;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Ninject;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace IdeasRepository.Web.Dependencies
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            _kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        public void AddBindings()
        {
            _kernel.Bind<IRecordsProvider>()
                .To<RecordsProvider>();

            _kernel.Bind<ApplicationDbContext>()
                .ToSelf();

            _kernel.Bind<IAccountsProvider>()
                .To<AccountsProvider>()
                .WithConstructorArgument<HttpContextBase>(HttpContext.Current?.Request.RequestContext.HttpContext);
            _kernel.Bind<IRolesManager>()
                .To<RolesProvider>()
                .WithConstructorArgument<HttpContextBase>(HttpContext.Current?.Request.RequestContext.HttpContext);

            _kernel.Bind<ApplicationUserManager>()
                .ToMethod(c => HttpContext.Current?.Request.RequestContext.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>());
            _kernel.Bind<ApplicationRoleManager>()
                .ToMethod(c => HttpContext.Current?.Request.RequestContext.HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>());
            _kernel.Bind<IAuthenticationManager>()
                .ToMethod(c => HttpContext.Current?.Request.RequestContext.HttpContext.GetOwinContext().Authentication);
        }
    }
}