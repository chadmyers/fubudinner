using FubuMVC.Core.View;

namespace FubuDinner.Web.Actions.Accounts
{
    public class RegisterAction
    {
        public RegisterModel Query(RegisterModel input)
        {
            //TODO: This is all bogus placeholder and will get tossed and TDD'd
            return new RegisterModel {PasswordLength = 6};
        }

        public RegisterForm Command(RegisterForm input)
        {
            //TODO: This is all bogus placeholder and will get tossed and TDD'd
            return input;
        }
    }

    public class RegisterForm : RegisterModel
    {
    }

    public class RegisterModel
    {
        public int PasswordLength { get; set; }

        /*[Required]*/
        public string Username { get; set; }
        /*[Required]*/
        public string Email { get; set; }
        /*[Required]*/
        public string Password { get; set; }
        /*[Required]*/
        public string ConfirmPassword { get; set; }
    }

    public class Register : FubuPage<RegisterModel>
    {
    }
}