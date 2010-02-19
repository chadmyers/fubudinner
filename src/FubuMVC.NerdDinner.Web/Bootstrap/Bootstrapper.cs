using System.Collections.Generic;
using System.Web.Routing;
using FubuMVC.Core.Runtime;
using FubuMVC.StructureMap;
using StructureMap;

namespace FubuMVC.NerdDinner.Web.Bootstrap
{
    public class Bootstrapper
    {
        private readonly ICollection<RouteBase> _routes;

        public Bootstrapper()
            : this(new List<RouteBase>())
        {
        }

        public Bootstrapper(ICollection<RouteBase> routes)
        {
            _routes = routes;
        }

        public void BootstrapStructureMap()
        {
            UrlContext.Reset();

            ObjectFactory.Initialize(x =>
            {
                x.UseDefaultStructureMapConfigFile = false;
            });

            BootstrapFubu(ObjectFactory.Container, _routes);
        }

        public static void BootstrapFubu(IContainer container, ICollection<RouteBase> routes)
        {
            var bootstrapper = new StructureMapBootstrapper(container, new WebRegistry());

            bootstrapper.Bootstrap(routes);
        }

        public static void Bootstrap()
        {
            new Bootstrapper().BootstrapStructureMap();
        }

        public static void Bootstrap(RouteCollection routes)
        {
            new Bootstrapper(routes).BootstrapStructureMap();
        }
    }
}