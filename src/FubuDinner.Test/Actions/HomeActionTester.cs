using FubuDinner.Web.Actions.Home;
using NUnit.Framework;

namespace FubuDinner.Test.Actions
{
    [TestFixture]
    public class HomeActionTester
    {
        [Test]
        public void should_return_model()
        {
            new HomeAction().Execute(null).ShouldNotBeNull();
        }
    }
}