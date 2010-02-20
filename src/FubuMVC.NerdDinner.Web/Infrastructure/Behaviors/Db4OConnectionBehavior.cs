using System;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Runtime;
using FubuMVC.StructureMap;
using StructureMap;

namespace FubuDinner.Web.Infrastructure.Behaviors
{
    public class Db4OConnectionBehavior : IActionBehavior
    {
        private readonly IContainer _container;
        private readonly ServiceArguments _arguments;
        private readonly Guid _behaviorId;

        public Db4OConnectionBehavior(IContainer container, ServiceArguments arguments, Guid behaviorId)
        {
            _container = container;
            _arguments = arguments;
            _behaviorId = behaviorId;
        }

        public void Invoke()
        {
            using( var nested = _container.GetNestedContainer() )
            {
                nested.GetInstance<IDb4OConnection>().Initialize();
                invokeRequestedBehavior(nested);
            }
        }

        private void invokeRequestedBehavior(IContainer c)
        {
            var behavior = c.GetInstance<IActionBehavior>(_arguments.ToExplicitArgs(), _behaviorId.ToString());
            behavior.Invoke();
        }

        public void InvokePartial()
        {
            // Just go straight to the inner behavior here.  Assuming that the transaction & principal
            // are already set up
            invokeRequestedBehavior(_container);
        }
    }
}