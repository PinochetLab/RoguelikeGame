using System;
using System.Collections.Generic;

namespace Roguelike.Actors.Enemies.AI.StateMachine;

/// <summary>
/// Defines the behaviour of a state of a finit state machine
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class StateBehaviour<T>
{
    private readonly List<Action<StateData<T>>> processCallbacks = new();
    private readonly List<Action> enterCallbacks = new();
    private readonly List<Action> leaveCallbacks = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="StateBehaviour{T}"/> class.
    /// </summary>
    /// <param name="state">The state.</param>
    internal StateBehaviour(T state)
    {
        State = state;
    }

    /// <summary>
    /// Gets the state associated with this behaviour
    /// </summary>
    public T State { get; private set; }

    /// <summary>
    /// Gets the time duration of the state (if any)
    /// </summary>
    public float? Duration { get; private set; }

    /// <summary>
    /// Gets the function which will be used to select the next state when this expires or Next() gets called.
    /// </summary>
    public Func<T> NextStateSelector { get; private set; }

    /// <summary>
    /// Sets a callback which will be called when the FSM enters in this state
    /// </summary>
    public StateBehaviour<T> OnEnter(Action callback)
    {
        enterCallbacks.Add(callback);
        return this;
    }

    /// <summary>
    /// Sets a callback which will be called when the FSM leaves this state
    /// </summary>
    public StateBehaviour<T> OnLeave(Action callback)
    {
        leaveCallbacks.Add(callback);
        return this;
    }

    /// <summary>
    /// Sets a callback which will be called everytime Process is called on the FSM, when this state is active
    /// </summary>
    public StateBehaviour<T> Calls(Action<StateData<T>> callback)
    {
        processCallbacks.Add(callback);
        return this;
    }

    /// <summary>
    /// Sets the state to automatically expire after the given time (in seconds)
    /// </summary>
    public StateBehaviour<T> Expires(float duration)
    {
        Duration = duration;
        return this;
    }

    /// <summary>
    /// Sets the state to which the FSM goes when the duration of this expires, or when Next() gets called on the FSM
    /// </summary>
    /// <param name="state">The state.</param>
    public StateBehaviour<T> GoesTo(T state)
    {
        NextStateSelector = () => state;
        return this;
    }

    /// <summary>
    /// Sets a function which selects the state to which the FSM goes when the duration of this expires, or when Next() gets called on the FSM
    /// </summary>
    /// <param name="stateSelector">The state selector function.</param>
    public StateBehaviour<T> GoesTo(Func<T> stateSelector)
    {
        NextStateSelector = stateSelector;
        return this;
    }


    /// <summary>
    /// Calls the process callback
    /// </summary>
    internal void Trigger(StateData<T> data)
    {
        for (int i = 0, len = processCallbacks.Count; i < len; i++)
            processCallbacks[i](data);
    }

    /// <summary>
    /// Calls the onenter callback
    /// </summary>
    internal void TriggerEnter()
    {
        for (int i = 0, len = enterCallbacks.Count; i < len; i++)
            enterCallbacks[i]();
    }

    /// <summary>
    /// Calls the onleave callback
    /// </summary>
    internal void TriggerLeave()
    {
        for (int i = 0, len = leaveCallbacks.Count; i < len; i++)
            leaveCallbacks[i]();
    }
}