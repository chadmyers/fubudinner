using System;
using FubuMVC.Core.Configuration;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace FubuDinner.Web.Infrastructure.Bootstrap
{
    public class SettingsScanner : IRegistrationConvention
    {
        public void Process(Type type, Registry graph)
        {
            if (!type.Name.EndsWith("Settings") || type.IsInterface) return;

            graph.For(type).Use(c => c.GetInstance<ISettingsProvider>().SettingsFor(type));
        }
    }
}