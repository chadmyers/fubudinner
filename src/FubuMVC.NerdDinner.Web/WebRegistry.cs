using System;
using FubuMVC.Core;
using FubuDinner.Web.Actions.Home;
using FubuDinner.Web.Infrastructure.Behaviors;
using FubuMVC.UI;

namespace FubuDinner.Web
{
    public class WebRegistry : FubuRegistry
    {
        public WebRegistry()
        {
            IncludeDiagnostics(true);

            Applies.ToThisAssembly();

            Actions
                .IncludeTypesNamed(x => x.EndsWith("Action"));

            Routes
                .IgnoreControllerNamespaceEntirely()
                .IgnoreClassSuffix("Action")
                .IgnoreMethodsNamed("Execute")
                .IgnoreMethodSuffix("Command")
                .IgnoreMethodSuffix("Query")
                .ConstrainToHttpMethod(action => action.Method.Name.EndsWith("Command"), "POST")
                .ConstrainToHttpMethod(action => action.Method.Name.StartsWith("Query"), "GET");

            Views
                .TryToAttach(x =>
                {
                    x.by_ViewModel_and_Namespace_and_MethodName();
                    x.by_ViewModel_and_Namespace();
                    x.by_ViewModel();
                });

            this.HtmlConvention<NerdHtmlConventions>();

            this.StringConversions(x =>
            {
                x.IfIsType<DateTime>(d => d.ToString("g"));
                x.IfIsType<decimal>(d => d.ToString("N2"));
                x.IfIsType<float>(f => f.ToString("N2"));
                x.IfIsType<double>(d => d.ToString("N2"));
            });

            HomeIs<HomeModel>();
        }
    }

    public class NerdHtmlConventions : HtmlConventionRegistry
    {
        public NerdHtmlConventions()
        {
            Editors
                .If(d => d.Accessor.InnerProperty.Name.EndsWith("itude"))
                .Attr("type", "hidden");
        }
    }
}