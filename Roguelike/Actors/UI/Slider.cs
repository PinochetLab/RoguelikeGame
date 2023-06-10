using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using Roguelike.Components.Sprites;
using Roguelike.Core;

namespace Roguelike.Actors.UI;
public class Slider : Actor, IActorCreatable<Slider>
{
    private Actor backgroundActor;
    private Actor fillActor;
    private SpriteComponent backgroundSC;
    private SpriteComponent fillSC;

    private float ratio = 0;

    public Slider(BaseGame game) : base(game)
    {
    }

    public static Slider Create(BaseGame game) => new Slider(game);


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

    public Vector2Int Offset { get; set; } = new Vector2Int(0, 30);

    public Vector2Int SliderSize { get; set; } = new Vector2Int(60, 10);


    public float Ratio
    {
        get => ratio;
        set
        {
            ratio = value;
            fillSC.Transform.Scale = new Vector2(ratio, 1);
        }
    }


    public override void OnStart()
    {
        base.OnStart();

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

    public override void Update(GameTime time)
    {
        base.Update(time);
    }
}
