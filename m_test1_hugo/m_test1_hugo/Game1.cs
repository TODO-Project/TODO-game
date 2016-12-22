using m_test1_hugo.Class.Main;
using m_test1_hugo.Class.Weapons;
using m_test1_hugo.Class.Tile_Engine;
using m_test1_hugo.Class.Main.Menus;
using m_test1_hugo.Class.Characters;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace m_test1_hugo
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public static int WindowHeight = 1080;
        public static int WindowWidth = 1920;

       /* public static int WindowHeight { get; set; }
        public static int WindowWidth { get; set; }*/


        SpriteBatch spriteBatch;
        Sniper sniper;
        Player player;
        GraphicsDeviceManager graphics;

        ///////////////////////////////////////////////////////////////////
        //////////    POLYGAME ////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////

        /*
                Sniper sniper2;
                Player player2;
                Sniper sniper3;
                Player player3;
                Sniper sniper4;
                Player player4;
                Sniper sniper5;
                Player player5;
        */

        ///////////////////////////////////////////////////////////////////
        //////////    POLYGAME ////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////

        /*
                internal Sniper Sniper
                {
                    get
                    {
                        return Sniper1;
                    }

                    set
                    {
                        Sniper1 = value;
                    }
                }

                internal Sniper Sniper1
                {
                    get
                    {
                        return sniper;
                    }

                    set
                    {
                        sniper = value;
                    }
                }
        */

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;

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
            // TODO: Add your initialization logic here
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

            player = new Player(new Sprinter());
            player.LoadContent(Content);

            sniper = new Sniper(player);
            sniper.LoadContent(Content);

            ///////////////////////////////////////////////////////////////////
            //////////    POLYGAME ////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////
/*
            player2 = new Player();
            player2.LoadContent(Content);
            player2.Position = new Vector2(1*player2.Width, 0);

            sniper2 = new Sniper(player2);
            sniper2.LoadContent(Content);

            player3 = new Player();
            player3.LoadContent(Content);
            player3.Position = new Vector2(2 * player3.Width, 0);

            sniper3 = new Sniper(player3);
            sniper3.LoadContent(Content);

            player4 = new Player();
            player4.LoadContent(Content);
            player4.Position = new Vector2(3 * player4.Width, 0);

            sniper4 = new Sniper(player4);
            sniper4.LoadContent(Content);

            player5 = new Player();
            player5.LoadContent(Content);
            player5.Position = new Vector2(4 * player5.Width, 0);

            sniper5 = new Sniper(player5);
            sniper5.LoadContent(Content);

            ///////////////////////////////////////////////////////////////////
            //////////    POLYGAME ////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////
            */
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
                Exit();
            // TODO: Add your update logic here

            player.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            spriteBatch.Begin();

            player.Draw(spriteBatch);
            sniper.Draw(spriteBatch);

            ///////////////////////////////////////////////////////////////////
            //////////    POLYGAME ////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////
/*
            player2.Draw(spriteBatch);
            sniper2.Draw(spriteBatch);

            player3.Draw(spriteBatch);
            sniper3.Draw(spriteBatch);

            player4.Draw(spriteBatch);
            sniper4.Draw(spriteBatch);

            player5.Draw(spriteBatch);
            sniper5.Draw(spriteBatch);
*/
            ///////////////////////////////////////////////////////////////////
            //////////    POLYGAME ////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
