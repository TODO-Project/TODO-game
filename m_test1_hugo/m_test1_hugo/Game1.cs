﻿using m_test1_hugo.Class.Main;
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
using m_test1_hugo.Class.Main.InputSouris;
using m_test1_hugo.Class.Characters.Teams;
using m_test1_hugo.Class.Main.outils_dev_jeu.ArmesVignette;
using Microsoft.Xna.Framework.Audio;
using m_test1_hugo.Class.clothes;

namespace m_test1_hugo
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        #region Graphics

        public static int WindowWidth = 720;
        public static int WindowHeight = 480;
        public static SpriteBatch spriteBatch;
        GraphicsDeviceManager graphics;
        Overlay overlay;
        Camera camera;

        #endregion

        #region mouse + keyboard
        public static MouseState ms;
        public static KeyboardState kb;

        #endregion

        #region map Variables

        TileEngine tileEngine = new TileEngine(32, 32);

        Tileset tileset;

        TileMap map;

        public static int mapWidth;
        public static int mapHeight;

        #endregion

        #region teams
        Team TeamBlue;
        Team TeamRed;
        #endregion

        #region Players
        public static Player player;
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
            #region teams intialization
            TeamBlue = new Team(1, "blue");
            TeamRed = new Team(2, "red");
            #endregion

            player = new Player(new Sprinter(), new Assault(), TeamBlue);
            // player.Position = new Vector2(50, 50);

            Shirt leatherShirt = new Shirt("leatherShirt", 0, 30) ;
            leatherShirt.interract(player);

            player.Health = 50;
            
           
            SpeedBuff speedBuff = new SpeedBuff();

            Heal heal = new Heal();
            heal.Position = new Vector2(150, 150);
            MagicBox box = new MagicBox();
            box.Position = new Vector2(50, 250);

            overlay = new Overlay();

            camera = new Camera(GraphicsDevice.Viewport);
            camera.Origin = player.Position;

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

            player.LoadContent(Content);

            #region Drawing Map
            Texture2D tilesetTexture = Content.Load<Texture2D>("terrain");
            tileset = new Tileset(tilesetTexture, 32, 32, 32, 32);

            /*
            // Couche 1
            List<string> maps = new List<string>();
            maps.Add("maps/start/1");
            maps.Add("maps/rocky/1");
            maps.Add("maps/beach/1");
            maps.Add("maps/lava/1");

            // Couche 2
            List<string> maps2 = new List<string>();
            maps2.Add("maps/start/2");
            maps2.Add("maps/rocky/2");
            maps2.Add("maps/beach/2");
            maps2.Add("maps/lava/2");

            // Couche de ponts 1
            List<string> ponts1 = new List<string>();
            ponts1.Add("maps/pont/droite");
            ponts1.Add("maps/pont/gauche");
            ponts1.Add("maps/pont/droite");
            ponts1.Add("maps/pont/gauche");

            // Couche de ponts 2
            List<string> ponts2 = new List<string>();
            ponts2.Add("maps/pont/bas");
            ponts2.Add("maps/pont/bas");
            ponts2.Add("maps/pont/haut");
            ponts2.Add("maps/pont/haut");
            */

            List<string> maps = new List<string>();
            maps.Add("maps/grassy32/1");
            maps.Add("maps/grassy32/1");
            maps.Add("maps/grassy32/1");
            maps.Add("maps/grassy32/1");

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

            List<int> ordreNormal = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                ordreNormal.Add(i);
            }

            /*
            // Map layer
            MapLayer layer = new MapLayer(maps, 16, ordre);
            MapLayer layer2 = new MapLayer(maps2, 16, ordre);
            MapLayer layerPonts1 = new MapLayer(ponts1, 16, ordreNormal);
            MapLayer layerPonts2 = new MapLayer(ponts2, 16, ordreNormal);
            

            var layers = new List<MapLayer>();
            layers.Add(layer);
            layers.Add(layer2);
            layers.Add(layerPonts1);
            layers.Add(layerPonts2);
            
            var tilesets = new List<Tileset>();
            tilesets.Add(tileset);

            map = new TileMap(tilesets, layers);
            mapWidth = map.GetWidth();
            mapHeight = map.GetHeight();
            */

            MapLayer layer = new MapLayer(maps, 32, ordre);
            var layers = new List<MapLayer>();
            layers.Add(layer);

            var tilesets = new List<Tileset>();
            tilesets.Add(tileset);

            map = new TileMap(tilesets, layers);
            mapWidth = map.GetWidth();
            mapHeight = map.GetHeight();

            #endregion

            overlay.LoadContent(Content);

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
            kb = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (kb.IsKeyDown(Keys.P))
            {
                new MagicBox().Position = new Vector2(50,150);
                //new Player(new Sprinter(), new Minigun(), TeamRed).Position = new Vector2(600, 600);
            }      

            if (kb.IsKeyDown(Keys.M))
            {
                player.godmode = !player.godmode;
            }

            camera.Position = player.Position - new Vector2(GraphicsDevice.Viewport.Width / 2f, GraphicsDevice.Viewport.Height / 2f);

            // TODO: Add your update logic here
            overlay.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            var viewMatrix = camera.GetViewMatrix();

            spriteBatch.Begin(transformMatrix: viewMatrix);

            map.Draw(spriteBatch);

            #region Drawing and updating Bonuses
            for (var i = 0; i < Bonus.BonusList.Count; i++)
            {
                var currentBonus = Bonus.BonusList[i];
                currentBonus.LoadContent(Content);
                currentBonus.Draw(spriteBatch);
                currentBonus.Update(gameTime);
                currentBonus.UpdateSprite(gameTime);
            }
            #endregion

            #region Drawing and updating players
            for (var i = 0; i < Character.CharacterList.Count; i++)
            {
                Player player = (Player)Character.CharacterList[i];
                player.LoadContent(Content);
                player.DrawCharacter(spriteBatch);
                player.Control(gameTime, 32, mapWidth * 2, mapHeight * 2, map.PCollisionLayer);
            }
            #endregion

            #region Drawing and updating bullets
            for (var i = 0; i < Bullet.BulletList.Count; i++)
            {
                Bullet currentBullet = (Bullet)Bullet.BulletList[i];

                // La texture n'etait pas charger et en plus le fichier bullet n'existe pas 😉
                currentBullet.LoadContent(Content);

                currentBullet.Update(gameTime, 32, mapWidth, mapHeight, map.BCollisionLayer);

                currentBullet.Draw(spriteBatch);
            }
            #endregion

            #region Drawing WeaponPics
            for (var i = 0; i < WeaponPic.WeaponPicList.Count; i++)
            {
                WeaponPic weaponPic = WeaponPic.WeaponPicList[i];
                weaponPic.LoadContent(Content);
                weaponPic.Draw(spriteBatch);
            }
            #endregion
            spriteBatch.End(); // fin spritebatch

            spriteBatch.Begin(); // tout ce qui ne bouge pas avec la camera

            overlay.Draw(spriteBatch);
            #region drawing clothes to body

            for(var i=0; i<player.ClothesList.Length;i++)
            {
                if(player.ClothesList[i] != null)
                {
                    player.ClothesList[i].LoadContent(Content);
                    player.ClothesList[i].Draw(spriteBatch);
                }
                
            }

            #endregion
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
