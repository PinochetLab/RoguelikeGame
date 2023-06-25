namespace Roguelike.Actors.Enemies.AI.StateMachine;

/// <summary>
///     Extension methods to easily use the FSM as a queueing of consecutive states.
/// </summary>
public static class MachineQueueExtensions
{
    /// <summary>
    ///     Queues a new state to specified FSM.
    /// </summary>
    /// <param name="stateMachine">The FSM.</param>
    /// <returns>The newly created state.</returns>
    public static StateBehaviour<int> Queue(this StateMachine<int> stateMachine)
    {
        return stateMachine.Add(stateMachine.Count)
            .GoesTo(stateMachine.Count);
    }

    /// <summary>
    ///     Queues a new state to specified FSM.
    /// </summary>
    /// <param name="stateMachine">The FSM.</param>
    /// <returns>The newly created state.</returns>
    public static StateBehaviour<long> Queue(this StateMachine<long> stateMachine)
    {
        return stateMachine.Add(stateMachine.Count)
            .GoesTo(stateMachine.Count);
    }

    /// <summary>
    ///     Queues a new state to specified FSM.
    /// </summary>
    /// <param name="stateMachine">The FSM.</param>
    /// <returns>The newly created state.</returns>
    public static StateBehaviour<uint> Queue(this StateMachine<uint> stateMachine)
    {
        return stateMachine.Add((uint)stateMachine.Count)
            .GoesTo((uint)stateMachine.Count);
    }

    /// <summary>
    ///     Queues a new state to specified FSM.
    /// </summary>
    /// <param name="stateMachine">The FSM.</param>
    /// <returns>The newly created state.</returns>
    public static StateBehaviour<ulong> Queue(this StateMachine<ulong> stateMachine)
    {
        return stateMachine.Add((ulong)stateMachine.Count)
            .GoesTo((ulong)stateMachine.Count);
    }

    /// <summary>
    ///     Queues a new state to specified FSM.
    /// </summary>
    /// <param name="stateMachine">The FSM.</param>
    /// <returns>The newly created state.</returns>
    public static StateBehaviour<short> Queue(this StateMachine<short> stateMachine)
    {
        return stateMachine.Add((short)stateMachine.Count)
            .GoesTo((short)stateMachine.Count);
    }

    /// <summary>
    ///     Queues a new state to specified FSM.
    /// </summary>
    /// <param name="stateMachine">The FSM.</param>
    /// <returns>The newly created state.</returns>
    public static StateBehaviour<ushort> Queue(this StateMachine<ushort> stateMachine)
    {
        return stateMachine.Add((ushort)stateMachine.Count)
            .GoesTo((ushort)stateMachine.Count);
    }

    /// <summary>
    ///     Queues a new state to specified FSM.
    /// </summary>
    /// <param name="stateMachine">The FSM.</param>
    /// <returns>The newly created state.</returns>
    public static StateBehaviour<byte> Queue(this StateMachine<byte> stateMachine)
    {
        return stateMachine.Add((byte)stateMachine.Count)
            .GoesTo((byte)stateMachine.Count);
    }

    /// <summary>
    ///     Queues a new state to specified FSM.
    /// </summary>
    /// <param name="stateMachine">The FSM.</param>
    /// <returns>The newly created state.</returns>
    public static StateBehaviour<sbyte> Queue(this StateMachine<sbyte> stateMachine)
    {
        return stateMachine.Add((sbyte)stateMachine.Count)
            .GoesTo((sbyte)(stateMachine.Count + 1));
    }

    /// <summary>
    ///     Queues a new state to specified FSM.
    /// </summary>
    /// <param name="stateMachine">The FSM.</param>
    /// <returns>The newly created state.</returns>
    public static StateBehaviour<char> Queue(this StateMachine<char> stateMachine)
    {
        return stateMachine.Add((char)stateMachine.Count)
            .GoesTo((char)(stateMachine.Count + 1));
    }
}