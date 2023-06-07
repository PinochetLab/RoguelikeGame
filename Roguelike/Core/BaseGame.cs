using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Roguelike.Field;

namespace Roguelike.Core;

public abstract class BaseGame : Game
{
    private List<IDrawable> drawables = new();

    private readonly List<IDrawable> drawablesForRemove = new();
    private readonly List<IDrawable> drawablesForAdd = new();

    protected readonly GraphicsDeviceManager Graphics;
    private BaseWorldComponent world;

    private readonly ConcurrentDictionary<string, Texture2D> textures = new();


    /// <summary>
    /// Текущий игровой мир
    /// </summary>
    public BaseWorldComponent World
    {
        get => world;
        protected set
        {
            Components.Remove(world);
            world = value;
            Components.Add(world);
        }
    }

    /// <summary>
    /// SpriteBatch, необходимый для отрисовки.
    /// </summary>
    public SpriteBatch SpriteBatch { get; private set; }

    /// <summary>
    /// Констуктор. Здесь происходит инициализация игрового окна.
    /// </summary>
    protected BaseGame()
    {
        Graphics = new GraphicsDeviceManager(this);
        Graphics.PreferredBackBufferWidth = FieldInfo.ScreenWith;
        Graphics.PreferredBackBufferHeight = FieldInfo.ScreenHeight;
    }

    /// <summary>
    /// Метод для получения текстуры по названию.
    /// </summary>
    public Texture2D GetTexture(string path)
    {
        if (textures.TryGetValue(path, out var texture))
            return texture;

        var value = Content.Load<Texture2D>(path);
        textures.TryAdd(path, value);
        return value;
    }

    /// <summary>
    /// Загрузка контента.
    /// </summary>
    protected override void LoadContent()
    {
        SpriteBatch = new SpriteBatch(GraphicsDevice);
    }

    /// <summary>
    /// Обновление игровой логики. gameTime - текущее игровое время.
    /// </summary>
    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        UpdateStatesOfDrawables();
    }

    /// <summary>
    /// Отрисовка.
    /// </summary>
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        base.Draw(gameTime);

        SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

        drawables = drawables.OrderBy(x => x.Canvas ? 100 : 0 + x.DrawOrder).ToList();
        drawables.ForEach(x => x.Draw(gameTime, SpriteBatch));

        SpriteBatch.End();
    }

    /// <summary>
    /// Метод отвечающий за добавление нового объекта, требующего отрисовки, для последующей работы с ним.
    /// </summary>
    public void AddDrawable(IDrawable drawable)
    {
        drawablesForAdd.Add(drawable);
    }

    /// <summary>
    /// Метод, отвечающий за удаление (забывание) объекта, требующего отрисовки.
    /// </summary>
    public void RemoveDrawable(IDrawable drawable)
    {
        drawablesForRemove.Add(drawable);
    }

    private void UpdateStatesOfDrawables()
    {
        drawablesForRemove.ForEach(x => drawables.Remove(x));
        drawablesForRemove.Clear();

        drawablesForAdd.ForEach(x => drawables.Add(x));
        drawablesForAdd.Clear();
    }
}