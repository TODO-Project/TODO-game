﻿using m_test1_hugo.Class.Bonuses;
using m_test1_hugo.Class.Characters;
using m_test1_hugo.Class.Characters.Teams;
using m_test1_hugo.Class.ControlLayouts;
using m_test1_hugo.Class.Main.outils_dev_jeu;
using m_test1_hugo.Class.Main.outils_dev_jeu.ArmesVignette;
using m_test1_hugo.Class.Main.outils_dev_jeu.ControlLayouts;
using m_test1_hugo.Class.Main.outils_dev_jeu.pics;
using m_test1_hugo.Class.Main.overlay;
using m_test1_hugo.Class.Tile_Engine;
using m_test1_hugo.Class.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;
using m_test1_hugo.Class.Network;
using System.Threading;
using m_test1_hugo.Class.Network.Messages;

namespace m_test1_hugo.Class.Main.Menus.pages
{
    public class GamePage : MenuPage
    {
        #region attributs
        #region graphics
        Overlay overlay;
        public static Camera camera;
        SpriteFont font;
        Vector2 BlueScorePosition
        {
            get
            {
                return new Vector2(Game1.WindowWidth / 2 - 50, 150);
            }
        }
        Vector2 RedScorePosition
        {
            get
            {
                return new Vector2(Game1.WindowWidth / 2 + 50, 150);
            }
        }
        #endregion

        #region mouse + keyboard
        public static MouseState ms;
        public static KeyboardState kb;
        public static GamePadState gp;
        private ControlLayout azerty = new Azerty();
        private ControlLayout qwerty = new Qwerty();
        private ControlLayout gamepad = new GamePadController();
        #endregion

        #region map Variables

        TileEngine tileEngine = new TileEngine(32, 32);

        Tileset tileset;

        TileMap map;

        public static int mapWidth;
        public static int mapHeight;
        public static int tilesize = 32;
        #endregion

        #region Players
        public static Player player;
        public static List<Player> PlayerList;
        public static List<Player> PlayersToDraw;
        private Texture2D bgtexture;
        private Vector2 bgPosition;
        #endregion

        #region bonuses
        public static List<Bonus> BonusList;
        public static List<Pics> PicList = new List<Pics>();
        #endregion

	#region Network
        public static Server server;
        public static Client client;
	#endregion

        #endregion

        private bool createOK;

        public GamePage(Weapon weapon, string Pseudo, Team team)
        {
            PlayerList = new List<Player>();
            PlayersToDraw = PlayerList;
            BonusList = new List<Bonus>();
            PicList = new List<Pics>();

            font = Game1.Content.Load<SpriteFont>("font");
            Console.WriteLine("gamepage created");
            // TODO: Add your initialization logic heres
            #region teams intialization
            #endregion

            #region Drawing Map
            Texture2D tilesetTexture = Game1.Content.Load<Texture2D>("terrain");
            tileset = new Tileset(tilesetTexture, 32, 32, 32, 32);

            List<string> maps = new List<string>();
            maps.Add("maps/grassy32/1");
            maps.Add("maps/paved32/1");
            maps.Add("maps/grassy32/1");
            maps.Add("maps/grassy32/1");

            List<string> maps2 = new List<string>();
            maps2.Add("maps/grassy32/2");
            maps2.Add("maps/paved32/2");
            maps2.Add("maps/grassy32/2");
            maps2.Add("maps/grassy32/2");

            List<string> maps3 = new List<string>();
            maps3.Add("maps/special/vide");
            maps3.Add("maps/paved32/3");
            maps3.Add("maps/special/vide");
            maps3.Add("maps/special/vide");

            List<string> ponts = new List<string>();
            ponts.Add("maps/pont/bas-droite");
            ponts.Add("maps/pont/bas-gauche");
            ponts.Add("maps/pont/haut-droite");
            ponts.Add("maps/pont/haut-gauche");

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

            MapLayer layer = new MapLayer(maps, 32, ordre);
            MapLayer layer2 = new MapLayer(maps2, 32, ordre);
            MapLayer layer3 = new MapLayer(maps3, 32, ordre);
            MapLayer pont = new MapLayer(ponts, 32, ordreNormal);

            var layers = new List<MapLayer>();
            layers.Add(layer);
            layers.Add(layer2);
            layers.Add(layer3);
            layers.Add(pont);

            var tilesets = new List<Tileset>();
            tilesets.Add(tileset);

            map = new TileMap(tilesets, layers);
            map.Tilesize = tilesize;
            mapWidth = map.GetWidth();
            mapHeight = map.GetHeight();
            #endregion
            createOK = true;
           
            new Player(Pseudo, new Sprinter(), weapon, team, azerty, Spawn.RandomVector(map));
            //new Player("test", new Sprinter(), new Minigun(), TeamRed, qwerty, Spawn.RandomVector(map));

            player = PlayerList[0];
            //player.Health = 20;

            //SpeedBuff speedBuff = new SpeedBuff();

            Heal heal = new Heal();
            heal.Position = Spawn.RandomVector(map);


            new MagicBox(Spawn.RandomVector(map));

            overlay = new Overlay(player);

            camera = new Camera(Game1.graphics.GraphicsDevice.Viewport);

            new Player(new Sprinter(), new Assault(), Team.TeamList[1], gamepad, Spawn.RandomVector(800,800));
            bgtexture = Game1.Content.Load<Texture2D>("bg");
            //overlay.LoadContent(Content);
        }

        public override MenuPage Action()
        {
	    #region Network

            if (server != null)
            {
                if (server.SvThread == null)
                {
                    server.SvThread = new Thread(server.HandleMessages);
                    server.SvThread.Name = "Server thread";
                    server.SvThread.Start();
                }
            }

            if (client.ClThread == null)
            {
                client.ClThread = new Thread(client.HandleMessage);
                client.ClThread.Name = "Client thread";
                client.ClThread.Start();
            }

            if (client.Pdata != null)
            {
                PlayerList[1].Health = client.Pdata.Health;
                PlayerList[1].MaxHealth = client.Pdata.MaxHealth;
                PlayerList[1].MouseRotationAngle = client.Pdata.MouseRotationAngle;
                float posx = client.Pdata.PosX;
                float posy = client.Pdata.PosY;
                PlayerList[1].Position = new Vector2(posx, posy);
            }

            if (client.GameClient.ConnectionStatus == NetConnectionStatus.Connected)
            {
                NetOutgoingMessage outmsg = client.GameClient.CreateMessage();
                PlayerDataGame pdata = new PlayerDataGame(player);
                pdata.EncodeMessage(outmsg);
                client.GameClient.SendMessage(outmsg, NetDeliveryMethod.ReliableOrdered);
            }

            #endregion
            Update();
            foreach(Team team in Team.TeamList)
            {
                if (team.TeamKills >= 10)
                {
                    SaveResults();
                    return new MainPage();
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.End();
            var viewMatrix = camera.GetViewMatrix();
            
            spriteBatch.Begin(transformMatrix: viewMatrix);

            bgPosition = camera.Origin;
            //spriteBatch.Draw(bgtexture, bgPosition, Color.White);

            map.Draw(spriteBatch);
            
            #region Drawing and updating Bonuses
            for (var i = 0; i < BonusList.Count; i++)
            {
                var currentBonus = BonusList[i];
                currentBonus.Draw(spriteBatch);
                currentBonus.Update(Game1.gameTime);
                currentBonus.UpdateSprite(Game1.gameTime);
            }
            #endregion

            #region Drawing and updating bullets
            for (var i = 0; i < Bullet.BulletList.Count; i++)
            {
                Bullet currentBullet = (Bullet)Bullet.BulletList[i];
                currentBullet.Update(Game1.gameTime, 32, mapWidth, mapHeight, map.BCollisionLayer);
                currentBullet.Draw(spriteBatch);
            }
            #endregion

            #region Drawing and updating players
            for (var i = 0; i < PlayerList.Count; i++)
            {
                Player player = PlayerList[i];
                player.Control(Game1.gameTime, 32, mapWidth, mapHeight, map.PCollisionLayer);
            }

            for (var i = 0; i < PlayersToDraw.Count; i++)
            {
                Player player = PlayerList[i];
                player.healthBar.Draw(spriteBatch);
                player.DrawCharacter(Game1.spriteBatch);
            }

            #endregion

            #region Drawing WeaponPics
            for (var i = 0; i < GamePage.PicList.Count; i++)
            {
                if (GamePage.PicList[i] is WeaponPic)
                {
                    WeaponPic weaponPic = (WeaponPic)GamePage.PicList[i];
                    weaponPic.Draw(spriteBatch);
                }
                else if (GamePage.PicList[i] is ClothPic)
                {
                    ClothPic clothPic = (ClothPic)GamePage.PicList[i];
                    clothPic.Draw(spriteBatch);
                }

            }
            #endregion
            spriteBatch.End(); // fin spritebatch

            spriteBatch.Begin(); // tout ce qui ne bouge pas avec la camera

            overlay.Draw(spriteBatch);
            #region drawing clothes to body

            for (var i = 0; i < player.ClothesList.Length; i++)
            {
                if (player.ClothesList[i] != null)
                {
                    player.ClothesList[i].Draw(spriteBatch);
                }

            }
            DrawTeamScores(spriteBatch);
            #endregion
        }

        public override void Update()
        {
            //overlay.Update(Game1.gameTime);
            ms = Mouse.GetState();
            kb = Keyboard.GetState();

            foreach (Team team in Team.TeamList)
                team.Update();

            if (player.IsDead())
            {
                if (kb.IsKeyDown(Keys.NumPad1))
                    player.Respawn(Spawn.RandomVector(map), PlayerList);
            }

            if (kb.IsKeyDown(Keys.P))
            {
                new ClothBox(Spawn.RandomVector(map));
                //player.weapon = new shotgun(player);
            }

            if (kb.IsKeyDown(Keys.I))
            {
                new MagicBox(Spawn.RandomVector(map));
                //player.weapon = new shotgun(player);
            }

            if (kb.IsKeyDown(Keys.O))
            {
                if (createOK)
                {
                    new Player(new Sprinter(), new Assault(), Team.TeamList[1], gamepad, Spawn.RandomVector(map));
                    new Player(new Sprinter(), new Assault(), Team.TeamList[0], gamepad, Spawn.RandomVector(map));
                    createOK = false;
                }
                    
            }
            if (kb.IsKeyUp(Keys.O))
                createOK = true;

            if (player.IsDead())
            {
                if(kb.IsKeyDown(Keys.NumPad1))
                {
                    player.Respawn(Spawn.RandomVector(map), PlayerList);
                }
            }
        }

        public void DrawTeamScores(SpriteBatch spriteBatch)
        {
            string blueScore = Team.TeamList[0].TeamKills.ToString();
            string redScore = Team.TeamList[1].TeamKills.ToString();
            spriteBatch.DrawString(font, blueScore, BlueScorePosition, Team.TeamList[0]._Color, 0f, font.MeasureString(blueScore), 0.6f, SpriteEffects.None, 0.3f ); //drawing blue score
            spriteBatch.DrawString(font, ":", new Vector2(BlueScorePosition.X + 30, 100), Color.White, 0f, font.MeasureString(":") / 2, 0.6f, SpriteEffects.None, 0.3f); 
            spriteBatch.DrawString(font, redScore, RedScorePosition, Team.TeamList[1]._Color, 0f, font.MeasureString(redScore), 0.6f, SpriteEffects.None, 0.3f); //drawing red score
        }

        private void SaveResults()
        {
            var player = GamePage.player;
            string text = "joueur : " + player.Pseudo + "      Score : " + player.Kills + "/" + player.Deaths +"      Heure :"+ DateTime.Now.Hour + ":" +DateTime.Now.Minute;
            StreamWriter swFile = new StreamWriter(@"../../../../scores/scores.txt", true);
            swFile.WriteLine(text);
            swFile.Close();
        }
    }
}