using FubuMVC.Core.View;

namespace FubuDinner.Web.Actions.Home
{
    public class HomeAction
    {
        public HomeModel Execute(HomeModel input)
        {
            return new HomeModel();
        }
    }

    public class Home : FubuPage<HomeModel>
    {
    }

    public class HomeModel
    {
    }
}