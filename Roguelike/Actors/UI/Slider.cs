using Microsoft.Xna.Framework;
using Roguelike.Components.Sprites;
using Roguelike.Core;

namespace Roguelike.Actors.UI;

public class Slider : Actor, IActorCreatable<Slider>
{
    private Actor backgroundActor;
    private SpriteComponent backgroundSC;
    private Actor fillActor;
    private SpriteComponent fillSC;

    private float ratio;

    public Slider(BaseGame game) : base(game)
    {
    }


    public Color BackgroundColor
    {
        get => backgroundSC.Color;
        set => backgroundSC.Color = value;
    }

    public Color FillColor
    {
        get => fillSC.Color;
        set => fillSC.Color = value;
    }

    public Vector2Int Offset { get; set; } = new(0, 30);

    public Vector2Int SliderSize { get; set; } = new(60, 10);


    public float Ratio
    {
        get => ratio;
        set
        {
            ratio = value;
            fillSC.Transform.Scale = new Vector2(ratio, 1);
        }
    }

    public static Slider Create(BaseGame game)
    {
        return new(game);
    }


    public override void Initialize()
    {
        base.Initialize();

        var offset = new Vector2Int(Offset.X, -Offset.Y);

        Transform.IsTile = false;

        backgroundActor = Game.World.CreateActor(Transform.Position + offset);

        backgroundActor.Transform.Parent = Transform;
        backgroundActor.Transform.IsTile = false;

        backgroundSC = backgroundActor.AddComponent<SpriteComponent>();
        backgroundSC.SetTexture("WhiteCell");
        backgroundSC.Size = SliderSize;
        backgroundSC.Canvas = true;

        fillActor = Game.World.CreateActor(Transform.Position + offset - new Vector2Int(SliderSize.X / 2, 0));
        fillActor.Transform.Parent = backgroundActor.Transform;
        fillActor.Transform.IsTile = false;


        fillSC = fillActor.AddComponent<SpriteComponent>();
        fillSC.SetTexture("WhiteCell");
        FillColor = Color.Red;
        fillSC.Size = SliderSize;
        fillSC.Pivot = new Vector2(0, 0.5f);
        Ratio = ratio;
        fillSC.Canvas = true;
        fillSC.DrawOrder = 1;
    }
}