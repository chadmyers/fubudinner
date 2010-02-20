using System.Collections.Generic;
using System.Web.Routing;
using FubuMVC.Core.Runtime;
using FubuDinner.Web.Infrastructure.Behaviors;
using FubuMVC.StructureMap;
using StructureMap;

namespace FubuDinner.Web.Infrastructure.Bootstrap
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

                                         x.Scan(s =>
                                                {
                                                    s.TheCallingAssembly();

                                                    // scan with a custom IRegistrationConvention
                                                    s.Convention<SettingsScanner>();
                                                });

                                         x.For<IDb4OConnection>().Use<Db4OConnection>();
                                         x.For<IRepository>().Use<Db4ORepository>();

                                     });

            BootstrapFubu(ObjectFactory.Container, _routes);
        }

        public static void BootstrapFubu(IContainer container, ICollection<RouteBase> routes)
        {
            var bootstrapper = new StructureMapBootstrapper(container, new WebRegistry())
                                   {
                                       Builder = (c, args, id) => new Db4OConnectionBehavior(c, args, id)
                                   };

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