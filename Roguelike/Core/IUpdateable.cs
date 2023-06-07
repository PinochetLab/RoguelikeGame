namespace Roguelike.Core;

/// <summary>
/// Интерфейс, отвечающий за всё, что обновляется каждый кадр.
/// </summary>
public interface IUpdateable
{
    /// <summary>
    /// Метод, вызывающийся каждый кадр.
    /// </summary>
    public void Update(float delta);
}
