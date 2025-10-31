namespace StateManaging
{
    public interface IState
    {
        void Enter();
        void Tick();
        void Exit();
    }
}