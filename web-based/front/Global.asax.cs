using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace front
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Error()
        {
            var exception = Server.GetLastError();
            if (exception is UnauthorizedAccessException)
            {
                Response.Redirect("~/Home/HomePage");
            }
        }
        protected void Application_Start()
        {

            ViewEngines.Engines.Clear();
            var razorEngine = new RazorViewEngine();
            razorEngine.ViewLocationFormats = razorEngine.MasterLocationFormats
                .Concat(new[]
                {
                    "`/View/Home/{1}/{0}.cshtml", //{0} = Action | {1} = Controller
                }).ToArray();
            ViewEngines.Engines.Add(razorEngine);

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
