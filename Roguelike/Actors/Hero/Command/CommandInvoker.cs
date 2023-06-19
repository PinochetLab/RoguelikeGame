using System.Collections.Generic;
using Roguelike.Core;

namespace Roguelike.Commands;

public class CommandInvoker : BaseGameSystem
{
    private readonly List<ICommand> commandHistory = new();
    private ICommand currentCommand;

    public CommandInvoker(BaseGame game) : base(game)
    {
    }

    public bool Valid => currentCommand != null;

    public void SetCommand(ICommand command)
    {
        currentCommand = command;
    }

    public void Invoke()
    {
        if (!Valid) return;
        commandHistory.Add(currentCommand);
        currentCommand.Execute();
        currentCommand = null;
    }
}