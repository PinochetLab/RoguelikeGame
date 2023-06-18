using System;
using System.Diagnostics;
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

    private Vector2Int offset = new(0, -30);

    private float ratio;

    private Vector2Int sliderSize = new(60, 10);

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

    public Vector2Int Offset
    {
        get => offset;
        set
        {
            offset = value;
            backgroundActor.Transform.Position = Transform.Position + offset;
        }
    }

    public Vector2Int SliderSize
    {
        get => sliderSize;
        set
        {
            sliderSize = value;
            backgroundSC.Size = sliderSize;
            fillSC.Size = sliderSize;
            fillActor.Transform.Position = backgroundActor.Transform.Position - new Vector2Int(sliderSize.X / 2, 0);
        }
    }


    public float Ratio
    {
        get => ratio;
        set
        {
            value = Math.Clamp(value, 0, 1);
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

        Transform.IsCanvas = true;

        backgroundActor = Game.World.CreateActor(Transform.Position + offset);

        backgroundActor.Transform.Parent = Transform;
        backgroundActor.Transform.IsCanvas = true;

        backgroundSC = backgroundActor.AddComponent<SpriteComponent>();
        backgroundSC.SetTexture("WhiteCell");
        backgroundSC.Size = SliderSize;

        fillActor = Game.World.CreateActor(Transform.Position + offset - new Vector2Int(SliderSize.X / 2, 0));
        Debug.WriteLine("create: " + (Transform.Position + offset - new Vector2Int(SliderSize.X / 2, 0)));
        fillActor.Transform.Parent = backgroundActor.Transform;
        fillActor.Transform.IsCanvas = true;


        fillSC = fillActor.AddComponent<SpriteComponent>();
        fillSC.SetTexture("WhiteCell");
        FillColor = Color.Red;
        fillSC.Size = SliderSize;
        fillSC.Pivot = new Vector2(0, 0.5f);
        Ratio = ratio;
        fillSC.DrawOrder = 1;
    }
}