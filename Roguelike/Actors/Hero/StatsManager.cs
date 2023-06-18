using Microsoft.Xna.Framework;
using Roguelike.Actors.UI;
using Roguelike.Components;
using Roguelike.Components.Sprites;
using Roguelike.Core;
using Roguelike.Field;

namespace Roguelike;

public class StatsManager : BaseGameSystem
{
    private const int LevelCount = 5;
    private static readonly int[] Healths = new int[LevelCount] { 100, 120, 150, 200, 300 };
    private static readonly int[] Experiences = new int[LevelCount] { 100, 120, 150, 200, 300 };

    private static readonly Color[] ExpColors = new Color[LevelCount]
    {
        Color.Blue,
        Color.Yellow,
        Color.Magenta,
        Color.Red,
        Color.Orange
    };

    private int currentExp;
    private int currentHealth;

    private int currentLevel;
    private Slider experienceSlider;

    private Slider healthSlider;
    private TextComponent levelText;
    private SpriteComponent starSpriteComponent;

    public StatsManager(BaseGame game) : base(game)
    {
    }

    public void SetHealth(int health)
    {
        currentHealth = health;
        UpdateInterface();
    }

    public void AddExperience(int exp)
    {
        currentExp += exp;
        UpdateInterface();
        TryUpgrade();
    }

    private void UpdateInterface()
    {
        healthSlider.Ratio = (float)currentHealth / Healths[currentLevel];
        experienceSlider.Ratio = (float)currentExp / Experiences[currentLevel];
        experienceSlider.FillColor = ExpColors[currentLevel];
        levelText.Text = (currentLevel + 1).ToString();
    }

    public bool TryUpgrade()
    {
        if (currentLevel >= LevelCount - 1)
        {
            currentLevel = LevelCount - 1;
            return false;
        }

        if (currentExp < Experiences[currentLevel]) return false;
        currentExp -= Experiences[currentLevel];
        currentLevel++;
        Game.World.Hero.UpdateHealth(Healths[currentLevel]);
        UpdateInterface();
        return true;
    }

    public override void Initialize()
    {
        base.Initialize();

        var healthSliderPosition = new Vector2Int(FieldInfo.ScreenWith / 2, FieldInfo.ScreenHeight - 110);
        healthSlider = Game.World.CreateActor<Slider>(healthSliderPosition);
        healthSlider.Offset = Vector2Int.Left * 20;
        healthSlider.SliderSize = new Vector2Int(500, 6);
        healthSlider.Ratio = 0.7f;
        healthSlider.FillColor = Color.Red;

        var experienceSliderPosition = new Vector2Int(FieldInfo.ScreenWith / 2, FieldInfo.ScreenHeight - 110);
        experienceSlider = Game.World.CreateActor<Slider>(healthSliderPosition);
        experienceSlider.Offset = Vector2Int.Up * 20 + Vector2Int.Left * 20;
        experienceSlider.SliderSize = new Vector2Int(500, 6);
        experienceSlider.Ratio = 0.7f;
        experienceSlider.FillColor = Color.Blue;

        var spritePosition = new Vector2Int(630, 778);
        var starActor = Game.World.CreateActor(spritePosition);
        starActor.Transform.IsCanvas = true;
        starSpriteComponent = starActor.AddComponent<SpriteComponent>();
        starSpriteComponent.SetTexture("Star");
        starSpriteComponent.Size = Vector2Int.One * 30;
        starSpriteComponent.DrawOrder = 7;

        var levelTextPosition = new Vector2Int(653, 763);
        var levelTextActor = Game.World.CreateActor(levelTextPosition);
        levelTextActor.Transform.IsCanvas = true;
        levelText = levelTextActor.AddComponent<TextComponent>();
        levelText.SetSpriteFont("MainFont");
    }
}