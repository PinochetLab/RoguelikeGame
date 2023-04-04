using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Roguelike
{
    public class RoguelikeGame : Game {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private static List<Actor> actors = new List<Actor>();
        private static List<Actor> actorsForRemove = new List<Actor>();

        private GameTime lastGameTime = new GameTime();

        public static RoguelikeGame instance;

        public SpriteBatch SpriteBatch { get => spriteBatch; }

        public static void AddActor(Actor actor) {
            actors.Add(actor);
        }

        public static void RemoveActor(Actor actor) {
            actorsForRemove.Add(actor);
        }

        public Texture2D LoadTexture(string path) {
            return Content.Load<Texture2D>(path);
        }

        private static void RemoveRemovedActors() {
            foreach (var actor in actorsForRemove) {
                actors.Remove(actor);
            }
            actorsForRemove.Clear();
        }

        public RoguelikeGame() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            instance = this;
        }

        protected override void Initialize() {
            base.Initialize();
            LoadContent();

            Hero.Spawn(Vector2.One);
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Debug.WriteLine((float)gameTime.ElapsedGameTime.TotalMilliseconds);

            var deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;

            base.Update(gameTime);

            Input.Update();

            foreach (var actor in actors) {
                actor.Update(deltaTime);
            }

            RemoveRemovedActors();

            lastGameTime = gameTime;
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);

            spriteBatch.Begin();

            foreach (var actor in actors) {
                actor.Draw(0);
            }

            spriteBatch.End();
        }
    }
}