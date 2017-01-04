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
using m_test1_hugo.Class.Main.Menus;

namespace m_test1_hugo
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public static int WindowHeight = 1080;
        public static int WindowWidth = 1920;

        TileEngine tileEngine = new TileEngine(32, 32);
        SpriteBatch spriteBatch;
        Sniper sniper;
        Player player;
        GraphicsDeviceManager graphics;
        TileMap map;
        List<int> ordre;

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

            // Tileset
            Texture2D tilesetTexture = Content.Load<Texture2D>("terrain");
            Tileset tileset = new Tileset(tilesetTexture, 32, 32, 32, 32);

            // Modules
            List<string> maps = new List<string>();
            maps.Add("maps/start/1");
            maps.Add("maps/start/1");
            maps.Add("maps/start/1");
            maps.Add("maps/start/1");

            // Système de génération de séquence aléatoire
            Random random = new Random();
            ordre = new List<int>();
            for (Int32 i = 0; i < maps.Count; i++)
            {
                int val = random.Next(0, maps.Count);
                while (ordre.Contains(val))
                {
                    val = random.Next(0, maps.Count);
                }
                ordre.Add(val);
            }

            // MapLayers
            MapLayer maplayer = new MapLayer(maps, 16, ordre);

            // TileMap
            map = new TileMap(tileset, maplayer);

          

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

            map.Draw(spriteBatch);

            player.Draw(spriteBatch);
            sniper.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
