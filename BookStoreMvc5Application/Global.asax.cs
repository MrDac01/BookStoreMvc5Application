using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using BookStoreMvc5Application.Classes;

namespace BookStoreMvc5Application
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            IoCConfig.RegisterDependencies();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            MappingConfig.Register();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = this.Server.GetLastError();
            HttpContext httpContext = ((HttpApplication)sender).Context;

            if (exception is HttpException httpException)
            {
                if (httpException.GetHttpCode() == 404)
                {
                    var errorHelper = DependencyResolver.Current.GetService<ErrorHelper>();
                    Response.Clear();
                    Server.ClearError();
                    //Хитрый обход ИИС, чтобы отображать свои красивые странички с ошибками
                    Response.TrySkipIisCustomErrors = true;

                    errorHelper.ProcessError(httpContext, httpException.GetHttpCode(), exception);
                }
            }
            //else
            //{
            //    errorHelper.ProcessError(httpContext, 500, exception);
            //}
        }
    }
}
