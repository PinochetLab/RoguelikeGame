using System;

namespace Roguelike.Actors.Enemies.AI.StateMachine;

/// <summary>
///     A Finite State Machine.
/// </summary>
public interface IStateMachine
{
    /// <summary>
    ///     Processes the logic for the FSM.
    /// </summary>
    /// <param name="time">The time, expressed in seconds.</param>
    /// <exception cref="InvalidOperationException"></exception>
    void ProcessIncremental(float dtime);

    /// <summary>
    ///     Processes the logic for the FSM.
    /// </summary>
    /// <param name="time">The time, expressed in seconds.</param>
    /// <exception cref="InvalidOperationException"></exception>
    void Process(float time);

    /// <summary>
    ///     Moves the FSM to the next state as configured using FsmStateBehaviour.GoesTo(...).
    ///     Note: to change the state freely, use the CurrentState property.
    /// </summary>
    /// <exception cref="InvalidOperationException">If the behaviour has not a next state / state selector configured.</exception>
    void Next();
}