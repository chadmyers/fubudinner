using FubuDinner.Web.Actions.Home;
using NUnit.Framework;

namespace FubuDinner.Test.Actions
{
    [TestFixture]
    public class AboutActionTester
    {
        [Test]
        public void should_return_model()
        {
            new AboutAction().Execute(null).ShouldNotBeNull();
        }
    }
}