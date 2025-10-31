using System;
using System.Collections.Generic;

namespace StateManaging
{
    public class StateMachine
    {
        private Type _currentStateType;
        private IState _currentState;

        private readonly Dictionary<Type, IState> _states = new();
        private readonly Dictionary<Type, List<ITransition>> _transitions = new();

        public StateMachine(IState[] states, ITransition[] transitions)
        {
            foreach (var state in states)
            {
                AddState(state);
            }

            foreach (var transition in transitions)
            {
                AddTransition(transition);
            }
        }

        public void AddState(IState state)
        {
            var type = state.GetType();
            if (!_states.TryAdd(type, state))
            {
                throw new InvalidOperationException($"State {type.Name} is already registered.");
            }
        }

        public void AddTransition(ITransition transition)
        {
            var fromType = transition.From;

            if (!_transitions.TryGetValue(fromType, out var list))
            {
                list = new List<ITransition>();
                _transitions[fromType] = list;
            }

            list.Add(transition);
        }

        public void ChangeState<T>() where T : IState
        {
            ChangeState(typeof(T));
        }

        private void ChangeState(Type targetType)
        {
            if (_currentStateType == targetType)
                return;

            if (!_states.TryGetValue(targetType, out var targetState))
            {
                throw new InvalidOperationException($"State {targetType.Name} is not registered.");
            }

            _currentState?.Exit();

            _currentStateType = targetType;
            _currentState = targetState;

            _currentState.Enter();
        }

        public void Tick()
        {
            _currentState?.Tick();

            if (_transitions.TryGetValue(_currentStateType, out var currentTransitions))
            {
                foreach (var transition in currentTransitions)
                {
                    if (transition.CheckCondition())
                    {
                        ChangeState(transition.To);
                        break;
                    }
                }
            }
        }
    }
}