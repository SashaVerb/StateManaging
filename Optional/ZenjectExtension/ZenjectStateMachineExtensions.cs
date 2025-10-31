using Zenject;

namespace StateManaging.Zenject
{
    public static class ZenjectStateMachineExtensions
    {
        public static void BindStateTransition<TFrom, TTo, TCondition>(
            this DiContainer container)
            where TFrom : IState
            where TTo : IState
            where TCondition : class, ICondition
        {
            var transitionType = typeof(Transition<TFrom, TTo>);

            container.BindInterfacesAndSelfTo(transitionType).AsSingle();

            container.BindInterfacesAndSelfTo<TCondition>().AsCached()
                     .WhenInjectedInto(transitionType);
        }
    }
}