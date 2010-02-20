using FubuMVC.Core.View;
using FubuDinner.Web.Model;

namespace FubuDinner.Web.Actions.Dinners
{
    public class CreateAction
    {
        public CreateDinner Query(CreateDinner input)
        {
            //TODO: This is all bogus placeholder and will get tossed and TDD'd
            return input;
        }

        public CreateDinner Command(CreateDinnerForm input)
        {
            //TODO: This is all bogus placeholder and will get tossed and TDD'd
            return new CreateDinner { Dinner = input.Dinner };
        }
    }

    public class CreateDinnerForm : CreateDinner
    {
    }

    public class CreateDinner
    {
        public Dinner Dinner { get; set; }
    }

    public class Create : FubuPage<CreateDinner>
    {
    }

}