using System.Collections.Generic;
using Roguelike.Core;

namespace Roguelike.Commands;

/// <summary>
///     Данный класс отвечает за обработку команд игрока
/// </summary>
public class CommandInvoker : BaseGameSystem
{
    private readonly List<ICommand> commandHistory = new();
    private ICommand currentCommand;

    public CommandInvoker(BaseGame game) : base(game)
    {
    }

    /// <summary>
    ///     Задана ли сейчас какая-то команда
    /// </summary>
    public bool Valid => currentCommand != null;

    /// <summary>
    ///     Задает команду
    /// </summary>
    public void SetCommand(ICommand command)
    {
        currentCommand = command;
    }

    /// <summary>
    ///     Запускает исполнение текущей команды
    /// </summary>
    public void Invoke()
    {
        if (!Valid) return;
        commandHistory.Add(currentCommand);
        currentCommand.Execute();
        currentCommand = null;
    }
}