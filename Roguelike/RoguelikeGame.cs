using Roguelike.VectorUtility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System;
using System.Net;
using MonoGame.Extended;
using Roguelike.Field;
using Roguelike.Actors.Wall;
using Roguelike.Actors;
using Roguelike.Actors.InventoryUtils;

namespace Roguelike
{
    public class RoguelikeGame : Game {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private static List<Actor> actors = new List<Actor>();
        private static List<Actor> actorsForRemove = new List<Actor>();

        private static List<CanvasActor> canvasActors = new List<CanvasActor>();
        private static List<CanvasActor> canvasActorsForRemove = new List<CanvasActor>();

        private GameTime lastGameTime = new GameTime();

        public static RoguelikeGame instance;

        private Texture2D floorTexture;
        private Texture2D wallTexture;

        public SpriteBatch SpriteBatch { get => spriteBatch; }

        public static void AddActor(Actor actor) {
            actors.Add(actor);
        }

        public static void AddCanvasActor(CanvasActor actor)
        {
            canvasActors.Add(actor);
        }

        public static void RemoveActor(Actor actor)
        {
            actorsForRemove.Add(actor);
        }

        public static void RemoveCanvasActor(CanvasActor actor) {
            canvasActorsForRemove.Add(actor);
        }

        public Texture2D LoadTexture(string path) {
            return Content.Load<Texture2D>(path);
        }

        private static void RemoveRemovedActors() {
            foreach (var actor in actorsForRemove) {
                actors.Remove(actor);
            }
            actorsForRemove.Clear();

            foreach (var actor in canvasActorsForRemove)
            {
                canvasActors.Remove(actor);
            }

            canvasActorsForRemove.Clear();
        }

        public RoguelikeGame() {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = FieldInfo.ScreenWith;
            graphics.PreferredBackBufferHeight = FieldInfo.ScreenHeight;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            instance = this;
        }

        protected override void Initialize() {
            base.Initialize();
            LoadContent();

            ItemHolder itemHolder = new ItemHolder(new Vector2Int(7, 3));

            Hero hero = new Hero(Vector2Int.One * (FieldInfo.CellCount / 2));


            for (int i = 0; i < FieldInfo.CellCount; i++) {
                new Wall(new Vector2Int(0, i));
                new Wall(new Vector2Int(FieldInfo.CellCount - 1, i));
                if (i > 0 && i < FieldInfo.CellCount - 1) {
                    new Wall(new Vector2Int(i, 0));
                    new Wall(new Vector2Int(i, FieldInfo.CellCount - 1));
                }
            }

            Vector2Int inventoryPosition = new Vector2Int(FieldInfo.ScreenWith / 2, FieldInfo.ScreenWith);
            Inventory inventory = new Inventory(inventoryPosition);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            floorTexture = Content.Load<Texture2D>("GrassTile");
            wallTexture = Content.Load<Texture2D>("Wall");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;

            base.Update(gameTime);

            foreach (var actor in actors)
            {
                actor.Update(deltaTime);
            }

            foreach (var actor in canvasActors)
            {
                actor.Update(deltaTime);
            }

            RemoveRemovedActors();

            lastGameTime = gameTime;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            var size = FieldInfo.ScreenWith;
            var cellCount = FieldInfo.CellCount;
            var cellSize = FieldInfo.CellSize;

            for (var i = 0; i < cellCount; i++)
            for (var j = 0; j < cellCount; j++)
            {
                var tex = floorTexture;
                var rectangle = new Rectangle(i * cellSize, j * cellSize, 1 * cellSize, 1 * cellSize);
                var rectSize = new Rectangle(0, 0, tex.Width, tex.Height);
                spriteBatch.Draw(tex, rectangle, rectSize, Color.WhiteSmoke);
            }

            foreach (var actor in actors)
                actor.Draw(0);

            for (var i = 1; i < cellCount; i++)
            {
                spriteBatch.DrawLine(new Vector2(0, i * cellSize), new Vector2(size, i * cellSize), Color.Black, 2);
                spriteBatch.DrawLine(new Vector2(i * cellSize, 0), new Vector2(i * cellSize, size), Color.Black, 2);
            }

            foreach (var actor in canvasActors)
                actor.Draw(0);

            spriteBatch.End();
        }
    }
}