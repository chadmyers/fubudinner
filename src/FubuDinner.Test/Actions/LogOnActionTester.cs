using FubuDinner.Web.Actions.Accounts;
using FubuDinner.Web.Actions.Home;
using FubuDinner.Web.Model;
using FubuMVC.Core.Continuations;
using FubuMVC.Core.Security;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuDinner.Test.Actions
{
    [TestFixture]
    public class LogOnActionQueryTester
    {
        private LogOnAction _action;

        [SetUp]
        public void SetUp()
        {
            _action = new LogOnAction(null, null, null, null);
        }

        [Test]
        public void should_return_return_url_from_input()
        {
            var input = new LogOnModel {ReturnUrl = "foo"};

            _action.Query(input).ReturnUrl.ShouldEqual("foo");
        }
    }

    [TestFixture]
    public class log_on_validation_fails : InteractionContext<LogOnAction>
    {
        private FubuContinuation _result;

        protected override void beforeEach()
        {
            UseInMemoryRepository();
            ValidationFailsFor<LogOnModelForm>();
            _result = ClassUnderTest.Command(new LogOnModelForm());
        }

        [Test]
        public void should_transfer_to_view_if_validation_fails()
        {
            _result.Destination<LogOnModel>().ShouldNotBeNull();
        }

        [Test]
        public void should_set_validation_errors_on_model_if_validation_fails()
        {
            var result = ClassUnderTest.Command(new LogOnModelForm());

            result.Destination<LogOnModel>().Errors.IsValid().ShouldBeFalse();
        }

        [Test]
        public void should_copy_form_values_to_output()
        {
            var input = new LogOnModelForm
                            {
                                Username = "username",
                                Password = "password",
                                rememberMe = "on",
                                ReturnUrl = "returnUrl"
                            };
            var result = ClassUnderTest.Command(input);

            var model = result.Destination<LogOnModel>();

            model.Username.ShouldEqual(input.Username);
            model.rememberMe.ShouldEqual(input.RememberUser);
            model.ReturnUrl.ShouldEqual(input.ReturnUrl);
        }

        [Test]
        public void should_not_copy_password_to_output()
        {
            var input = new LogOnModelForm{ Password = "password" };
            var result = ClassUnderTest.Command(input);

            var model = result.Destination<LogOnModel>();
            
            model.Password.ShouldBeNull();
        }
    }

    [TestFixture]
    public class log_on_validation_succeeds : InteractionContext<LogOnAction>
    {
        private LogOnModelForm _form;
        private FubuContinuation _result;

        protected override void beforeEach()
        {
            UseInMemoryRepository();
            ValidationSucceedsFor<LogOnModelForm>();
            Repository.Save(new Nerd { Username = "Nerd1", Password = "Password" });
            Repository.ClearHistory();
            _form = new LogOnModelForm {Username = "Nerd1", Password = "Password"};
            _result = ClassUnderTest.Command(_form);
        }

        [Test]
        public void should_transfer_to_view_with_errors_if_username_not_found()
        {
            _form.Username = "missing";

            _result = ClassUnderTest.Command(_form);

            _result.Destination<LogOnModel>().Errors.IsValid("Username").ShouldBeFalse();
        }

        [Test]
        public void should_transfer_to_view_with_errors_if_password_invalid()
        {
            _form.Password = "wrong";

            _result = ClassUnderTest.Command(_form);

            _result.Destination<LogOnModel>().Errors.IsValid("Username").ShouldBeFalse();
        }

        [Test]
        public void should_redirect_to_return_url()
        {
            _form.ReturnUrl = "url";

            _result = ClassUnderTest.Command(_form);

            _result.AssertWasRedirectedTo("url");
        }

        [Test]
        public void should_redirect_to_home_if_no_return_url_specified()
        {
            _result.Destination<HomeModel>().ShouldNotBeNull();
        }

        [Test]
        public void should_set_authentication_context()
        {
            MockFor<IAuthenticationContext>()
                .AssertWasCalled(c=>c.ThisUserHasBeenAuthenticated("Nerd1", false));
        }

        [Test]
        public void should_set_rememberMe_setting_on_authentication_context()
        {
            _form.rememberMe = "on";

            _result = ClassUnderTest.Command(_form);

            MockFor<IAuthenticationContext>()
                .AssertWasCalled(c => c.ThisUserHasBeenAuthenticated("Nerd1", true));
        }
    }
}