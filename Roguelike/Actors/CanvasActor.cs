using Roguelike.Components;
using Roguelike.VectorUtility;

namespace Roguelike.Actors;

public class CanvasActor : Actor
{
    public CanvasActor(Vector2Int position) : base(position)
    {
        Transform = AddComponent<TransformComponent>();
        Transform.Position = position;
        RoguelikeGame.AddCanvasActor(this);
        OnStart();
    }

    public override void Destroy()
    {
        RoguelikeGame.RemoveCanvasActor(this);
    }
}
