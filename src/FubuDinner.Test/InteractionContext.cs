using System;
using FubuDinner.Web.Infrastructure;
using FubuDinner.Web.Infrastructure.Validation;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;
using Rhino.Mocks.Interfaces;
using StructureMap;
using StructureMap.AutoMocking;

namespace FubuDinner.Test
{
    public class InteractionContext<T> where T : class 
    {
        private RhinoAutoMocker<T> _services;

        [SetUp]
        public void SetUp()
        {
            _services = new RhinoAutoMocker<T>(MockMode.AAA);
            beforeEach();
        }

        protected virtual void beforeEach()
        {
        }

        public InMemoryRepository Repository { get; private set; }

        public void UseInMemoryRepository()
        {
            Repository = new InMemoryRepository();
            Services.Inject<IRepository>(Repository);
        }


        public IContainer Container
        {
            get
            {
                return Services.Container;
            }
        }

        public RhinoAutoMocker<T> Services
        {
            get { return _services; }
        }

        public T ClassUnderTest
        {
            get
            {
                return _services.ClassUnderTest;
            }
        }

        public SERVICE MockFor<SERVICE>() where SERVICE : class
        {
            return _services.Get<SERVICE>();
        }

        public SERVICE StubFor<SERVICE>() where SERVICE : class
        {
            var service = MockRepository.GenerateStub<SERVICE>();
            _services.Inject(service);
            return service;
        }

        public void VerifyCallsFor<MOCK>() where MOCK : class
        {
            MockFor<MOCK>().VerifyAllExpectations();
        }

        public void AssertWasCalled<MOCK>(Action<MOCK> expectedMethod) where MOCK : class
        {
            MockFor<MOCK>().AssertWasCalled(expectedMethod);
        }

        public IMethodOptions<RhinoMocksExtensions.VoidType> MockFor<SERVICE>(Action<SERVICE> action) where SERVICE : class
        {
            return MockFor<SERVICE>().Expect(action);
        }

        public IMethodOptions<R> MockFor<SERVICE, R>(Function<SERVICE, R> func) where SERVICE : class
        {
            return MockFor<SERVICE>().Expect(func);
        }

        public void ValidationSucceedsFor<ENTITY>()
        {
            MockFor<IValidator>().Stub(x => x.Validate(null))
               .Return(Notification.Valid())
               .Constraints(Is.TypeOf(typeof(ENTITY)));
        }

        public void ValidationFailsFor<ENTITY>()
        {
            MockFor<IValidator>().Stub(x => x.Validate(null))
               .Return(Notification.Invalid())
               .Constraints(Is.TypeOf(typeof(ENTITY)));
        }
    }
}