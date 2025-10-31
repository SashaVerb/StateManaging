using System;

namespace StateManaging
{
    public interface ITransition
    {
        Type From { get; }
        Type To { get; }
        bool CheckCondition();
    }
}