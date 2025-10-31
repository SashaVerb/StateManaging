using System;

namespace StateManaging
{
    public class Transition<TFrom, TTo> : ITransition
        where TFrom : IState
        where TTo : IState
    {
        public Type From => typeof(TFrom);

        public Type To => typeof(TTo);

        private ICondition condition;

        protected Transition(ICondition condition)
        {
            this.condition = condition;
        }

        public bool CheckCondition()
        {
            return condition.Check();
        }
    }
}