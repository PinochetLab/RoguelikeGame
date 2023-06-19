namespace Roguelike.Actors.Enemies.AI.StateMachine;

public struct StateMachineSnapshot<T>
{
    internal T CurrentState { get; }
    internal float StateAge { get; }

    internal StateMachineSnapshot(float age, T currentState)
    {
        CurrentState = currentState;
        StateAge = age;
    }
}