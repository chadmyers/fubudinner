using FubuDinner.Web.Actions.Accounts;
using FubuDinner.Web.Infrastructure.Behaviors;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.Urls;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuDinner.Test.Infrastructure.Behaviors
{
    [TestFixture]
    public class MustBeAuthenticatedBehaviorTester : InteractionContext<MustBeAuthenticatedBehavior>
    {
        protected override void beforeEach()
        {
            FubuRequest.Set(new ReturnUrlModel { RawUrl = "return_url" });
            ClassUnderTest.InsideBehavior = MockFor<IActionBehavior>();
        }

        [Test]
        public void should_not_continue_if_not_authenticated()
        {
            ClassUnderTest.Invoke();

            MockFor<IActionBehavior>().AssertWasNotCalled(b => b.Invoke());
        }

        [Test]
        public void should_transfer_to_logon_if_not_authenticated()
        {
            MockFor<IUrlRegistry>().Stub(r => r.UrlFor(Arg<LogOnModel>.Is.NotNull)).Return("url");
            
            ClassUnderTest.Invoke();

            MockFor<IOutputWriter>().AssertWasCalled(c => c.RedirectToUrl("url"));
        }

        [Test]
        public void should_remember_return_url_when_transferring()
        {
            ClassUnderTest.Invoke();

            MockFor<IUrlRegistry>().AssertWasCalled(r =>
                r.UrlFor(Arg<LogOnModel>.Matches(m => m.ReturnUrl == "return_url")));
        }
    }
}