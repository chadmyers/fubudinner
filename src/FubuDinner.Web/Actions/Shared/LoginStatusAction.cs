using System.Security.Principal;
using System.Web;
using FubuMVC.Core;
using FubuMVC.Core.View;

namespace FubuDinner.Web.Actions.Shared
{
    public class LoginStatusAction
    {
        [FubuPartial]
        public LoginStatusModel Execute(LoginStatusModel input)
        {
            //TODO: This is all bogus placeholder and will get tossed and TDD'd
            return new LoginStatusModel
                       {
                           IsAuthenticated = input.IsAuthenticated,
                           UserName = getIdentityName(HttpContext.Current.User.Identity),
                           RawUrl = input.RawUrl
                       };
        }

        private static string getIdentityName(IIdentity identity)
        {
            return identity == null ? null : identity.Name;
        }
    }

    public class LoginStatusModel
    {
        public string RawUrl{ get; set;}
        public string UserName { get; set; }
        public bool IsAuthenticated { get; set; }
    }

    public class LoginStatus : FubuPage<LoginStatusModel>
    {
    }
}