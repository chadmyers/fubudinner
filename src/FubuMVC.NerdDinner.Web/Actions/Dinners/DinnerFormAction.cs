using System;
using FubuMVC.Core;
using FubuMVC.Core.View;
using FubuDinner.Web.Model;

namespace FubuDinner.Web.Actions.Dinners
{
    public class DinnerFormAction
    {
        [FubuPartial]
        public DinnerFormModel Execute(DinnerFormModel input)
        {
            //TODO: This is all bogus placeholder and will get tossed and TDD'd
            var hasDinner = input.Dinner.EventDate != DateTime.MinValue;

            var dinner = hasDinner ? input.Dinner : new Dinner { EventDate = DateTime.Now.AddDays(7) };

            return new DinnerFormModel { Dinner = dinner };
        }
    }

    public class DinnerFormModel
    {
        public Dinner Dinner { get; set; }
    }

    public class DinnerForm : FubuPage<DinnerFormModel>
    {
    }

}