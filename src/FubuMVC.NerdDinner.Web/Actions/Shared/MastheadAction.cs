using FubuMVC.Core;
using FubuMVC.Core.View;

namespace FubuDinner.Web.Actions.Shared
{
    public class MastheadAction
    {
        [FubuPartial]
        public MastheadModel Execute(MastheadModel input)
        {
            return input;
        }
    }

    public class MastheadModel
    {
        public bool ShowSearch { get; set; }
    }

    public class Masthead : FubuPage<MastheadModel>
    {
    }

}