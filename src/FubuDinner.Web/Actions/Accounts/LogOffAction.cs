using FubuMVC.Core.Continuations;
using FubuMVC.Core.View;
using FubuDinner.Web.Actions.Home;

namespace FubuDinner.Web.Actions.Accounts
{
    public class LogOffAction
    {
        public FubuContinuation Execute(LogOffModel input)
        {
            return FubuContinuation.RedirectTo(new HomeModel());
        }
    }

    public class LogOffModel
    {
    }

    public class LogOff : FubuPage<LogOffModel>
    {
    }
}