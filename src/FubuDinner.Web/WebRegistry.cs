using System;
using FubuDinner.Web.Actions.Accounts;
using FubuDinner.Web.Actions.Dinners;
using FubuDinner.Web.Infrastructure.Behaviors;
using FubuDinner.Web.Infrastructure.Validation;
using FubuMVC.Core;
using FubuDinner.Web.Actions.Home;
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

            Policies
                .ConditionallyWrapBehaviorChainsWith<MustBeAuthenticatedBehavior>(
                    c => c.HandlerType.Namespace.Equals(typeof(DinnersAction).Namespace)
                );
        }
    }

    public class NerdHtmlConventions : HtmlConventionRegistry
    {
        public NerdHtmlConventions()
        {
            numbers();
            validationAttributes();

            Editors
                .If(d => d.Accessor.InnerProperty.Name.EndsWith("itude"))
                .Attr("type", "hidden");
        }

        private void numbers()
        {
            Editors.IfPropertyIs<Int32>().Attr("max", Int32.MaxValue);
            Editors.IfPropertyIs<Int16>().Attr("max", Int16.MaxValue);
            Editors.IfPropertyIs<Int64>().Attr("max", Int64.MaxValue);
            Editors.IfPropertyTypeIs(IsIntegerBased).AddClass("integer");
            Editors.IfPropertyTypeIs(IsFloatingPoint).AddClass("number");
        }

        private void validationAttributes()
        {
            Editors.AddClassForAttribute<RequiredAttribute>("required");
            Editors.ModifyForAttribute<MaximumStringLengthAttribute>((tag, att) =>
            {
                if (att.Length < 2000)
                {
                    tag.Attr("maxlength", att.Length);
                }
            });
        }

        public static bool IsIntegerBased(Type type)
        {
            return type == typeof(int) || type == typeof(long) || type == typeof(short);
        }

        public static bool IsFloatingPoint(Type type)
        {
            return type == typeof(decimal) || type == typeof(float) || type == typeof(double);
        }
    }
}