using Roguelike.Components;
using Roguelike.VectorUtility;

namespace Roguelike.Actors;

public class CanvasActor : Actor
{
    public override void Initialize(Vector2Int position)
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
