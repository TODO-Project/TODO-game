using m_test1_hugo.Class.Characters;
using m_test1_hugo.Class.Main;
using m_test1_hugo.Class.Main.InputSouris;
using m_test1_hugo.Class.Main.Menus;
using m_test1_hugo.Class.Tile_Engine;
using m_test1_hugo.Class.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace m_test1_hugo
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        #region Graphics
        public static int WindowWidth = 1440;
        public static int WindowHeight = 900;
        public static SpriteBatch spriteBatch;
        GraphicsDeviceManager graphics;
        #endregion

        #region mouseVariables
        public static MouseState ms;
        public static double _rotationAngle;
        public double RotationAngle
        {
            get { return _rotationAngle; }
            set { _rotationAngle = value; }
        }
        #endregion

        #region map Variables

        TileEngine tileEngine = new TileEngine(32, 32);

        Tileset tileset;

        TileMap map;

        int mapWidth;
        int mapHeight;

        #endregion

        #region Players
        Player player;
        #endregion

        #region bullets
        Bullet bullet;
        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;

            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;
            //graphics.IsFullScreen = true;

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic heres
            player = new Player(new Sprinter(), new Sniper());

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

            for (var i = 0; i < Bullet.BulletList.Count; i++)
            {
                Bullet.BulletList[i].LoadContent(Content);
            }

            player.LoadContent(Content);

            Texture2D tilesetTexture = Content.Load<Texture2D>("terrain");
            tileset = new Tileset(tilesetTexture, 32, 32, 32, 32);

            // Couche 1
            List<string> maps = new List<string>();
            maps.Add("maps/start/1");
            maps.Add("maps/start/1");
            maps.Add("maps/lava/1");
            maps.Add("maps/lava/1");

            // Couche 2
            List<string> maps2 = new List<string>();
            maps2.Add("maps/start/2");
            maps2.Add("maps/start/2");
            maps2.Add("maps/lava/2");
            maps2.Add("maps/lava/2");

            // Système de génération de séquence aléatoire
            Random random = new Random();
            List<int> ordre = new List<int>();
            for (Int32 i = 0; i < maps.Count; i++)
            {
                int val = random.Next(0, maps.Count);
                while (ordre.Contains(val))
                {
                    val = random.Next(0, maps.Count);
                }
                ordre.Add(val);
            }

            // Map layer
            MapLayer layer = new MapLayer(maps, 16, ordre);
            MapLayer layer2 = new MapLayer(maps2, 16, ordre);

            var layers = new List<MapLayer>();
            layers.Add(layer);
            layers.Add(layer2);
            
            var tilesets = new List<Tileset>();
            tilesets.Add(tileset);

            map = new TileMap(tilesets, layers);
            mapWidth = map.GetWidth();
            mapHeight = map.GetHeight();

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
            ms = Mouse.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (ms.LeftButton == ButtonState.Pressed)
                player.shoot();

            // TODO: Add your update logic here

            for (var i = 0; i < Bullet.BulletList.Count; i++)
            {
                Bullet.BulletList[i].Update(gameTime);
            }

            player.MovePlayer(gameTime, 32, mapWidth, mapHeight, map.PCollisionLayer);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            map.Draw(spriteBatch);


            for (var i = 0; i < Bullet.BulletList.Count; i++)
            {
                Bullet currentBullet = (Bullet)Bullet.BulletList[i];

                // La texture n'etait pas charger et en plus le fichier bullet n'existe pas 😉
                currentBullet.LoadContent(Content);

                currentBullet.Draw(spriteBatch);
            }

            player.DrawPlayer(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
