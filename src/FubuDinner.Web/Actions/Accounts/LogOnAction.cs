using FubuDinner.Web.Infrastructure.Validation;
using FubuMVC.Core.View;

namespace FubuDinner.Web.Actions.Accounts
{
    public class LogOnAction
    {
        public LogOnModel Query(LogOnModel input)
        {
            //TODO: This is all bogus placeholder and will get tossed and TDD'd
            return input;
        }

        public LogOnModelForm Command(LogOnModelForm input)
        {
            //TODO: This is all bogus placeholder and will get tossed and TDD'd
            return input;
        }
    }

    public class LogOnModelForm : LogOnModel
    {
    }

    public class LogOnModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public bool rememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class LogOn : FubuPage<LogOnModel>
    {
    }
}