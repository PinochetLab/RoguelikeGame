namespace Roguelike.Commands;

/// <summary>
///     Данный интерфейс отвечает за команду игрока
/// </summary>
public interface ICommand
{
    public void Execute();
}