using m_test1_hugo.Class.Main;
using m_test1_hugo.Class.Characters;
using m_test1_hugo.Class.Tile_Engine;
using m_test1_hugo.Class.Weapons;
using m_test1_hugo.Class.Bonuses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using m_test1_hugo.Class.Main.overlay;
using m_test1_hugo.Class.Characters.Teams;
using m_test1_hugo.Class.Main.outils_dev_jeu.ArmesVignette;
using Microsoft.Xna.Framework.Audio;
using m_test1_hugo.Class.clothes;
using entrainementProjet1.Class.Main;
using m_test1_hugo.Class.Main.outils_dev_jeu.pics;
using m_test1_hugo.Class.Main.outils_dev_jeu;
using m_test1_hugo.Class.ControlLayouts;
using m_test1_hugo.Class.Main.outils_dev_jeu.ControlLayouts;
using m_test1_hugo.Class.Main.outils_dev_jeu.Affects;
using Lidgren.Network;
using Microsoft.Xna.Framework.Content;
using m_test1_hugo.Class.Main.Menus;
using m_test1_hugo.Class.Main.Menus.pages;
using m_test1_hugo.Class.gamestates.pages;

namespace m_test1_hugo
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public new static ContentManager Content;
        #region Graphics
        public static int WindowWidth;
        public static int WindowHeight;
        public static SpriteBatch spriteBatch;
        public static GraphicsDeviceManager graphics;
        public static GameTime gameTime = new GameTime();
        public GameState gameState;
        #endregion
        private FrameCounter frameCounter = new FrameCounter();
        public SpriteFont font;

        public Game1()
        {
            Content = base.Content;
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this);
            this.IsMouseVisible = true;
            WindowWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width; 
            WindowHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;
           
    }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            gameState = new GameState();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("arial");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
       
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                if (GamePage.server != null)
                {
                    GamePage.server.RequestStop();
                }

                if (GamePage.client != null)
                {
                    GamePage.client.RequestStop();
                }
                Exit();
            }

            if (gameState.activePage is ExitPage)
                Exit();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Teal);
            spriteBatch.Begin();

            gameState.Draw(spriteBatch);
            #region Drawing fps
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            frameCounter.Update(deltaTime);
            var fps = string.Format("FPS: {0}", frameCounter.AverageFPS);
            spriteBatch.DrawString(font, fps, new Vector2(WindowWidth - 150, 1), Color.Black, 0f, new Vector2(0, 0), 0.2f, SpriteEffects.None, 1f);
            #endregion

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}