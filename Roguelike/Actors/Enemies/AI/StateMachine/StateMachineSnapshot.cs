namespace Roguelike.Actors.Enemies.AI.StateMachine;

public struct StateMachineSnapshot<T>
{
    internal T CurrentState { get; private set; }
    internal float StateAge { get; private set; }

    internal StateMachineSnapshot(float age, T currentState)
    {
        CurrentState = currentState;
        StateAge = age;
    }
}