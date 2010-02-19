using FubuMVC.Core.View;

namespace FubuMVC.NerdDinner.Web.Actions.Home
{
    public class HomeAction
    {
        public HomeViewModel Home(HomeInputModel input)
        {
            return new HomeViewModel();
        }
    }

    public class Home : FubuPage<HomeViewModel>
    {
        
    }

    public class HomeInputModel
    {
    }

    public class HomeViewModel
    {
    }
}