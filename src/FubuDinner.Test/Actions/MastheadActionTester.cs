using FubuDinner.Web.Actions.Shared;
using NUnit.Framework;

namespace FubuDinner.Test.Actions
{
    [TestFixture]
    public class MastheadActionTester
    {
        [Test]
        public void should_default_to_not_displaying_the_search_area()
        {
            new MastheadAction().Execute(new MastheadModel()).ShowSearch.ShouldBeFalse();
        }

        [Test]
        public void should_respect_switch_for_showing_search_area()
        {
            var model = new MastheadModel{ShowSearch = true};
            new MastheadAction().Execute(model).ShowSearch.ShouldBeTrue();
        }
    }
}