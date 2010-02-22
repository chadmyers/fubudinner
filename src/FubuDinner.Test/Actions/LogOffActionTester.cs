using FubuDinner.Web.Actions.Accounts;
using FubuDinner.Web.Actions.Home;
using FubuMVC.Core.Continuations;
using FubuMVC.Core.Security;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuDinner.Test.Actions
{
    [TestFixture]
    public class LogOffActionTester : InteractionContext<LogOffAction>
    {
        private FubuContinuation _result;

        protected override void beforeEach()
        {
            _result = ClassUnderTest.Execute(null);
        }

        [Test]
        public void should_log_user_out_of_auth_context()
        {
            MockFor<IAuthenticationContext>().AssertWasCalled(c => c.SignOut());
        }

        [Test]
        public void should_redirect_to_home_page()
        {
            _result.Destination<HomeModel>().ShouldNotBeNull();
        }
    }
}