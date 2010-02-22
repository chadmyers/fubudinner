using FubuMVC.Core.Continuations;
using FubuMVC.Core.Security;
using FubuMVC.Core.View;
using FubuDinner.Web.Actions.Home;

namespace FubuDinner.Web.Actions.Accounts
{
    public class LogOffAction
    {
        private readonly IAuthenticationContext _authContext;

        public LogOffAction(IAuthenticationContext authContext)
        {
            _authContext = authContext;
        }

        public FubuContinuation Execute(LogOffModel input)
        {
            _authContext.SignOut();

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