using FubuMVC.Core;
using FubuMVC.NerdDinner.Web.Actions.Home;

namespace FubuMVC.NerdDinner.Web
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
                .ConstrainToHttpMethod(action => action.Method.Name.EndsWith("Command"), "POST")
                .ConstrainToHttpMethod(action => action.Method.Name.StartsWith("Query"), "GET");

            Views
                .TryToAttach(x =>
                {
                    x.by_ViewModel_and_Namespace_and_MethodName();
                    x.by_ViewModel_and_Namespace();
                    x.by_ViewModel();
                });

            HomeIs<HomeInputModel>();
        }
    }
}