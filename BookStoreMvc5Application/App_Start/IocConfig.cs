using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Autofac;
using Autofac.Integration.Mvc;

using BookStoreMvc5Application.Classes;

namespace BookStoreMvc5Application
{
    public class IoCConfig
    {
        /// <summary>
        /// For more info see 
        /// http://autofac.readthedocs.org/en/latest/integration/mvc.html
        /// </summary>
        public static void RegisterDependencies()
        {
            #region Create the builder
            var builder = new ContainerBuilder();
            #endregion

            builder.RegisterFilterProvider();
            builder.RegisterSource(new ViewRegistrationSource());

            // Register your SignalR hubs.
            //builder.RegisterHubs(typeof(MvcApplication).Assembly);

                #region Register all controllers for the assembly
            // Note that ASP.NET MVC requests controllers by their concrete types, 
            // so registering them As<IController>() is incorrect. 
            // Also, if you register controllers manually and choose to specify 
            // lifetimes, you must register them as InstancePerDependency() or 
            // InstancePerHttpRequest() - ASP.NET MVC will throw an exception if 
            // you try to reuse a controller instance for multiple requests. 
            builder.RegisterControllers(typeof(MvcApplication).Assembly)
                    .PropertiesAutowired()
                    .InstancePerDependency();

            //builder.RegisterApiControllers(typeof(MvcApplication).Assembly)
            //        .PropertiesAutowired()
            //        .InstancePerLifetimeScope();

            #endregion

            #region Register modules
            builder.RegisterAssemblyModules(typeof(MvcApplication).Assembly);
            #endregion

            #region Model binder providers - excluded - not sure if need
            //builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            //builder.RegisterModelBinderProvider();
            #endregion

            #region Inject HTTP Abstractions
            /*
			 The MVC Integration includes an Autofac module that will add HTTP request 
			 lifetime scoped registrations for the HTTP abstraction classes. The 
			 following abstract classes are included: 
			-- HttpContextBase 
			-- HttpRequestBase 
			-- HttpResponseBase 
			-- HttpServerUtilityBase 
			-- HttpSessionStateBase 
			-- HttpApplicationStateBase 
			-- HttpBrowserCapabilitiesBase 
			-- HttpCachePolicyBase 
			-- VirtualPathProvider 

			To use these abstractions add the AutofacWebTypesModule to the container 
			using the standard RegisterModule method. 
			*/
            builder.RegisterModule<AutofacWebTypesModule>();

            #endregion

            #region Set the MVC dependency resolver to use Autofac
            var container = builder.Build();

            DependencyResolver.SetResolver(new Autofac.Integration.Mvc.AutofacDependencyResolver(container));
            //GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            // Set the dependency resolver to be Autofac.
            //GlobalHost.DependencyResolver = new Autofac.Integration.SignalR.AutofacDependencyResolver(container);

            //Autofac.Integration.Wcf.AutofacHostFactory.Container = container;

            #endregion
        }
    }

    public class RegisterApplicationIoC : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).As<IAuthenticationManager>();


            //builder.RegisterType<AuthenticatorService<long>>()
            //    .As<IAuthenticatorService<long>>()
            //    .InstancePerLifetimeScope();

            //builder.RegisterType<UserAccessorImpl<long>>()
            //    .As<IUserProvider<long>>()
            //    .InstancePerDependency();

            //builder.RegisterType<DefaultCacheProvider>()
            //    .As<ICacheProvider>()
            //    .InstancePerLifetimeScope();

            builder.RegisterType<ErrorHelper>()
                .AsSelf()
                .SingleInstance();

        }
    }
}