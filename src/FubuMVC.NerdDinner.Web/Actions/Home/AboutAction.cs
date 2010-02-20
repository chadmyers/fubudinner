using FubuMVC.Core.View;

namespace FubuDinner.Web.Actions.Home
{
    public class AboutAction
    {
        public AboutModel Execute(AboutModel input)
        {
            return new AboutModel();
        }
    }

    public class AboutModel
    {
    }

    public class About : FubuPage<AboutModel>
    {
    }
}