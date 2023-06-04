using Roguelike.Components.Sprites;
using Roguelike.VectorUtility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.Actors.InventoryUtils;

public class Image : CanvasActor
{
    private ImageComponent imageComponent;

    private Vector2Int size;

    private string textureName = Inventory.NoneName;
    public Image(Vector2Int position, Vector2Int size) : base(position)
    {
        this.size = size;
    }

    public void LoadTexture(string name)
    {
        if (name == Inventory.NoneName || name == textureName) return;
        imageComponent.LoadTexture(name);
        imageComponent.Size = size;
        textureName = name;
    }

    public override void OnStart()
    {
        base.OnStart();

        imageComponent = new ImageComponent(this, size);
    }

    public override void Draw(float deltaTime)
    {
        base.Draw(deltaTime);

        imageComponent.Draw();
    }
}