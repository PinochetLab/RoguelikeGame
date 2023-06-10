using System.Collections.Generic;
using Roguelike.Core;

namespace Roguelike.Commands;
public class CommandInvoker : BaseGameSystem
{
    private ICommand currentCommand;

    private List<ICommand> commandHistory = new List<ICommand>();

    public CommandInvoker(BaseGame game) : base(game) { }

    public void SetCommand(ICommand command)
    {
        currentCommand = command;
    }

    public bool Valid => currentCommand != null;

    public void Invoke()
    {
        if (!Valid) return;
        commandHistory.Add(currentCommand);
        currentCommand.Execute();
        currentCommand = null;
    }
}
