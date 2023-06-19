using System;
using System.Collections.Generic;

namespace Roguelike.Actors.Enemies.AI.StateMachine;

/// <summary>
///     A Finite State Machine.
///     T is a type which will be used as descriptors of the state. Usually this is an enum, string or an integral type,
///     but any type can be used.
/// </summary>
/// <typeparam name="T">
///     A type which will be used as descriptors of the state. Usually this is an enum, string or an integral type,
///     but any type can be used.
/// </typeparam>
public class StateMachine<T> : IStateMachine
{
    private readonly string name;
    private readonly Dictionary<T, StateBehaviour<T>> stateBehaviours = new();

    private T currentState;
    private StateBehaviour<T> currentStateBehaviour;
    private float stateAge = -1f;
    private float timeBaseForIncremental;

    /// <summary>
    ///     Initializes a new instance of the <see cref="StateMachine{T}" /> class.
    /// </summary>
    /// <param name="name">The name of the FSM, used in throw exception and for debug purposes.</param>
    public StateMachine(string name)
    {
        this.name = name;
    }


    /// <summary>
    ///     Gets or sets a callback which will be called when the FSM logs state transitions. Used to track state transition
    ///     for debug purposes.
    /// </summary>
    /// <value>
    ///     The debug log handler.
    /// </value>
    public Action<string> DebugLogHandler { get; set; }

    /// <summary>
    ///     Gets the number of states currently in the FSM.
    /// </summary>
    /// <value>
    ///     The number of states currently in the FSM.
    /// </value>
    public int Count => stateBehaviours.Count;

    /// <summary>
    ///     Gets or sets the current state of the FSM.
    /// </summary>
    public T CurrentState
    {
        get => currentState;
        set => InternalSetCurrentState(value, true);
    }

    /// <summary>
    ///     Processes the logic for the FSM.
    /// </summary>
    /// <param name="time">The time, expressed in seconds.</param>
    /// <exception cref="InvalidOperationException"></exception>
    public void ProcessIncremental(float dtime)
    {
        timeBaseForIncremental += dtime;
        Process(timeBaseForIncremental);
    }

    /// <summary>
    ///     Processes the logic for the FSM.
    /// </summary>
    /// <param name="time">The time, expressed in seconds.</param>
    /// <exception cref="InvalidOperationException"></exception>
    public void Process(float time)
    {
        if (stateAge < 0f)
            stateAge = time;

        var totalTime = time;
        var stateTime = totalTime - stateAge;
        var stateProgress = 0f;

        if (currentStateBehaviour == null)
            throw new InvalidOperationException(
                $"[FSM {name}] : Can't call 'Process' before setting the starting state.");

        if (currentStateBehaviour.Duration.HasValue)
            stateProgress = Math.Max(0f, Math.Min(1f, stateTime / currentStateBehaviour.Duration.Value));

        var data = new StateData<T>
        {
            Machine = this,
            Behaviour = currentStateBehaviour,
            State = currentState,
            StateTime = stateTime,
            AbsoluteTime = totalTime,
            StateProgress = stateProgress
        };

        currentStateBehaviour.Trigger(data);

        if (stateProgress >= 1f && currentStateBehaviour.NextStateSelector != null)
        {
            CurrentState = currentStateBehaviour.NextStateSelector();
            stateAge = time;
        }
    }


    /// <summary>
    ///     Moves the FSM to the next state as configured using FsmStateBehaviour.GoesTo(...).
    ///     Note: to change the state freely, use the CurrentState property.
    /// </summary>
    /// <exception cref="InvalidOperationException">If the behaviour has not a next state / state selector configured.</exception>
    public void Next()
    {
        if (currentStateBehaviour.NextStateSelector != null)
            CurrentState = currentStateBehaviour.NextStateSelector();
        else
            throw new InvalidOperationException(string.Format("[FSM {0}] : Can't call 'Next' on current behaviour.",
                name));
    }


    /// <summary>
    ///     Adds the specified state.
    /// </summary>
    /// <param name="state">The state.</param>
    /// <returns>The newly created behaviour, so that it could be configured with a fluent-like syntax.</returns>
    public StateBehaviour<T> Add(T state)
    {
        var behaviour = new StateBehaviour<T>(state);
        stateBehaviours.Add(state, behaviour);
        return behaviour;
    }

    private void InternalSetCurrentState(T value, bool executeSideEffects)
    {
        if (DebugLogHandler != null)
            DebugLogHandler(string.Format("[FSM {0}] : Changing state from {1} to {2}", name, currentState, value));

        if (currentStateBehaviour != null && executeSideEffects)
            currentStateBehaviour.TriggerLeave();

        stateAge = -1f;

        currentStateBehaviour = stateBehaviours[value];
        currentState = value;

        if (currentStateBehaviour != null && executeSideEffects)
            currentStateBehaviour.TriggerEnter();
    }

    /// <summary>
    ///     Saves a snapshot of the FSM
    /// </summary>
    /// <returns>The snapshot.</returns>
    public StateMachineSnapshot<T> SaveSnapshot()
    {
        return new StateMachineSnapshot<T>(stateAge, currentState);
    }

    /// <summary>
    ///     Restores a snapshot of the FSM taken with SaveSnapshot
    /// </summary>
    /// <param name="snap">The snapshot.</param>
    public void RestoreSnapshot(StateMachineSnapshot<T> snap, bool executeSideEffects)
    {
        InternalSetCurrentState(snap.CurrentState, executeSideEffects);
        stateAge = snap.StateAge;
    }

    public StateBehaviour<T> GetBehaviour(T state)
    {
        return stateBehaviours[state];
    }
}