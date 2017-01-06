using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Main.Menus
{
    public abstract class Menu : Game
    {
        internal GraphicsDeviceManager graphics;
        internal SpriteBatch spriteBatch;
        protected Texture2D menuBackground;
        internal List<Button> buttonList;

        public static Vector2 curMousePos
        {
            get { return new Vector2(Mouse.GetState().X, Mouse.GetState().Y); }
        }

        public Menu()
        {
            graphics = new GraphicsDeviceManager(this);
            this.IsMouseVisible = true;

            graphics.PreferredBackBufferHeight = Game1.WindowHeight;
            graphics.PreferredBackBufferWidth = Game1.WindowWidth;

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }
    }
}