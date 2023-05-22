using System;
using VContainer;

namespace Code.Infrastructure.GameStateMachine
{
    public class StateFactory
    {
        private readonly IObjectResolver _objectResolver;

        public StateFactory(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }

        public IState Create(Type type)
        {
            if (typeof(IState).IsAssignableFrom(type))
            {
                return _objectResolver.Resolve<IState>();
            }
            throw new ArgumentException();
        }
    }
}