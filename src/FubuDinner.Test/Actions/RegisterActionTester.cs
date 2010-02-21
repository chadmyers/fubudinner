using FubuDinner.Web.Actions.Accounts;
using FubuDinner.Web.Infrastructure;
using FubuDinner.Web.Model;
using NUnit.Framework;

namespace FubuDinner.Test.Actions
{
    [TestFixture]
    public class RegisterActionQueryTester
    {
        private RegisterAction _action;
        private AccountSettings _settings;

        [SetUp]
        public void SetUp()
        {
            _settings = new AccountSettings();
            _action = new RegisterAction(_settings, null, null);
        }

        [Test]
        public void should_use_configured_min_password_length()
        {
            _settings.MinPasswordLength = 99;
            _action.Query(null).PasswordLength.ShouldEqual(99);
        }
    }

    [TestFixture]
    public class RegisterActionCommandTester : InteractionContext<RegisterAction>
    {
        private RegisterForm _form;

        protected override void beforeEach()
        {
            UseInMemoryRepository();

            _form = new RegisterForm {Nerd = new Nerd()};
        }

        [Test]
        public void validation_failure_should_be_reported()
        {
            ValidationFailsFor<RegisterForm>();
            
            var result = ClassUnderTest.Command(_form);

            result.Errors.IsValid().ShouldBeFalse();
        }

        [Test]
        public void should_fail_validation_if_password_and_confirm_password_do_not_match()
        {
            ValidationSucceedsFor<RegisterForm>(); // e.g. all the other (normal) validation succeeds

            _form.Nerd.Password = "foo";
            _form.ConfirmPassword = "bar";

            var result = ClassUnderTest.Command(_form);

            result.Errors.IsValid().ShouldBeFalse();
        }

        [Test]
        public void should_not_save_nerd_if_there_are_any_validation_errors()
        {
            ValidationFailsFor<RegisterForm>();

            ClassUnderTest.Command(_form);

            Repository.SavedItems.ShouldBeEmpty();
        }

        [Test]
        public void should_save_nerd_if_validation_succeeds()
        {
            ValidationSucceedsFor<RegisterForm>();

            ClassUnderTest.Command(_form);
            
            Repository.SavedItems.ShouldContain(_form.Nerd);
        }
    }
}