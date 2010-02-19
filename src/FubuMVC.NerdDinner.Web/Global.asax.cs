using System;
using System.Web;
using System.Web.Routing;
using FubuMVC.Core;
using FubuMVC.NerdDinner.Web.Bootstrap;

namespace FubuMVC.NerdDinner.Web
{
    public class Global : HttpApplication
    {
        public void Application_Start()
        {
            var routes = RouteTable.Routes;
            Bootstrapper.Bootstrap(routes);
        }

        protected void Application_OnError()
        {
            if (Context.IsCustomErrorEnabled && isAjaxRequest())
            {
                // By default, customErrors will cause a 302 redirect
                // We really want a 500 response, so that the client request knows there was an error
                error_without_redirect();
            }
        }

        private void error_without_redirect()
        {
            Server.ClearError();
            Response.ClearContent();
            Response.StatusCode = 500;
            Response.StatusDescription = "Internal Server Error";
            Response.Write("<html><body><h1>500 Internal Server Error</h1></body></html>");
        }

        private bool isAjaxRequest()
        {
            return HttpContext.Current.IsAjaxRequest();
        } 
    }
}