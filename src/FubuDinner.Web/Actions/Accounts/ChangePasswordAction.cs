using FubuMVC.Core.View;

namespace FubuDinner.Web.Actions.Accounts
{
    public class ChangePasswordAction
    {
        public ChangePasswordModel Query(ChangePasswordModel input)
        {
            //TODO: This is all bogus placeholder and will get tossed and TDD'd
            return new ChangePasswordModel { PasswordLength = 6 };
        }

        public ChangePasswordForm Command(ChangePasswordForm input)
        {
            //TODO: This is all bogus placeholder and will get tossed and TDD'd
            return input;
        }
    }

    public class ChangePasswordForm : ChangePasswordModel
    {
    }

    public class ChangePasswordModel
    {
        public int PasswordLength { get; set; }

        /*[Required]*/
        public string CurrentPassword { get; set; }
        /*[Required]*/
        public string NewPassword { get; set; }
        /*[Required]*/
        public string ConfirmPassword { get; set; }
    }

    public class ChangePassword : FubuPage<ChangePasswordModel>
    {
    }
}