using m_test1_hugo.Class.Bonuses;
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
using m_test1_hugo.Class.Characters.Classes;

namespace m_test1_hugo.Class.Main.Menus.pages
{
    /// <summary>
    /// GameState : le Jeu
    /// </summary>
    public class GamePage : MenuPage
    {
        #region attributs
        #region graphics
        Overlay overlay;
        ScoresOverlay scOverlay;
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
        public static ControlLayout azerty = new Azerty();
        public static ControlLayout qwerty = new Qwerty();
        public static ControlLayout gamepad = new GamePadController();
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
        private static  Random idrand = new Random(Guid.NewGuid().GetHashCode());
        public static Player player;
        public static List<Player> PlayerList;
        public static List<Player> PlayersToDraw;
        private Texture2D bgtexture;
        private Vector2 bgPosition;
        public static long unique_ID = idrand.Next();
        #endregion

        #region bonuses
        public static List<Bonus> BonusList;
        public static List<Pics> PicList = new List<Pics>();
        #endregion

	#region Network
        public static Server server;
        public static Client client;
	#endregion
	
	#region teleporters
	Teleporter tel1, tel2, tel3, tel4, tel5, tel6, tel7, tel8, tel9, tel10, tel11, tel12, tel13, tel14, tel15, tel16;
	#endregion

        #endregion

        private bool createOK;

        public GamePage(Weapon weapon, string Pseudo, Team team, CharacterClass classe)
        {
            Bullet.BulletList = new List<Bullet>();
            PlayerList = new List<Player>();
            PlayersToDraw = new List<Player>();
            BonusList = new List<Bonus>();
            PicList = new List<Pics>();
            scOverlay = new ScoresOverlay();

            font = Game1.Content.Load<SpriteFont>("font");
            Console.WriteLine("gamepage created");
            // TODO: Add your initialization logic heres
            #region teams intialization
            #endregion

            #region Récupération seed
            if (server != null)
            {
                server.SvThread = new Thread(server.HandleMessages);
                server.SvThread.Name = "Init server thread";
                server.SvThread.Start();
            }

            client.ClThread = new Thread(client.HandleMessage);
            client.ClThread.Name = "Init client thread";
            client.ClThread.Start();

            while (client.MapSeed == 0)
            {
                NetOutgoingMessage outmsg = client.GameClient.CreateMessage();
                GetMapSeed getmapseed = new GetMapSeed();
                getmapseed.EncodeMessage(outmsg);
                NetSendResult res = client.GameClient.SendMessage(outmsg, NetDeliveryMethod.ReliableOrdered);
                Thread.Sleep(500);
            }

            #endregion

            #region Envoi arrivée

            client.ClThread = new Thread(client.HandleMessage);
            client.ClThread.Name = "Init client thread 2";
            client.ClThread.Start();

            while (!client.IsConnected)
            {
                NetOutgoingMessage outmsg = client.GameClient.CreateMessage();
                SendArrival arrival;
                if (Pseudo == null)
                    arrival = new SendArrival("Jean-Kevin", team._teamNumber, weapon.Name);
                else
                    arrival = new SendArrival(Pseudo, team._teamNumber, weapon.Name);

                arrival.EncodeMessage(outmsg);
                NetSendResult res = client.GameClient.SendMessage(outmsg, NetDeliveryMethod.ReliableOrdered);
                Thread.Sleep(500);
            }

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
            Random random = new Random(client.MapSeed);
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
            //layers.Add(pont);

            var tilesets = new List<Tileset>();
            tilesets.Add(tileset);

            map = new TileMap(tilesets, layers);
            map.Tilesize = tilesize;
            mapWidth = map.GetWidth();
            mapHeight = map.GetHeight();
            #endregion
            createOK = true;
           
            player = new Player(Pseudo, classe, weapon, team, azerty, Spawn.RandomVector(map));
            player.weapon.CurrentAmmo = player.weapon.MagazineSize;
            PlayerList.Add(player);
            PlayersToDraw.Add(player);
            player.Id = unique_ID;
            if (player.weapon is Glock)
                player.weapon = new Glock(player);

            if (player.weapon is Fal)
                player.weapon = new Fal(player);

            if (player.weapon is Sniper)
                player.weapon = new Sniper(player);
            
            Heal heal = new Heal();
            heal.Position = Spawn.RandomVector(map);


            new MagicBox(Spawn.RandomVector(map));

            overlay = new Overlay(player);

            camera = new Camera(Game1.graphics.GraphicsDevice.Viewport);

            //new Player(new Sprinter(), new Assault(), Team.TeamList[1], gamepad, Spawn.RandomVector(800,800));
            bgtexture = Game1.Content.Load<Texture2D>("bg");
            
			
			tel1 = new Teleporter(new Vector2(955, 200), Teleporter.Side.Right);
			tel2 = new Teleporter(new Vector2(1040, 200), Teleporter.Side.Left);
			tel3 = new Teleporter(new Vector2(955, 750), Teleporter.Side.Right);
			tel4 = new Teleporter(new Vector2(1040, 750), Teleporter.Side.Left);
			tel5 = new Teleporter(new Vector2(200, 955), Teleporter.Side.Up);
			tel6 = new Teleporter(new Vector2(200, 1040), Teleporter.Side.Down);
			tel7 = new Teleporter(new Vector2(750, 955), Teleporter.Side.Up);
			tel8 = new Teleporter(new Vector2(750, 1040), Teleporter.Side.Down);
			tel9 = new Teleporter(new Vector2(1240, 955), Teleporter.Side.Up);
			tel10 = new Teleporter(new Vector2(1240, 1040), Teleporter.Side.Down);
			tel11 = new Teleporter(new Vector2(1790, 955), Teleporter.Side.Up);
			tel12 = new Teleporter(new Vector2(1790, 1040), Teleporter.Side.Down);
			tel13 = new Teleporter(new Vector2(955, 1240), Teleporter.Side.Right);
			tel14 = new Teleporter(new Vector2(1040, 1240), Teleporter.Side.Left);
			tel15 = new Teleporter(new Vector2(955, 1790), Teleporter.Side.Right);
			tel16 = new Teleporter(new Vector2(1040, 1790), Teleporter.Side.Left);
			tel1.setLink(tel2);
			tel3.setLink(tel4);
			tel5.setLink(tel6);
			tel7.setLink(tel8);
			tel9.setLink(tel10);
			tel11.setLink(tel12);
			tel13.setLink(tel14);
			tel15.setLink(tel16);
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

            if (client.RecievedPlayerData != null)
            {
                if (PlayerList.Count > 1)
                {
                    Player p = PlayerList.Find(x => x.Id == client.RecievedPlayerData.ID);
                    if (p != null)
                        client.RecievedPlayerData.TransferDataToPlayer(p);
                }
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
            var viewMatrix = camera.GetViewMatrix(this);
            
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
            Player bad_player = PlayerList.Find(x => x.Id == 0);
            if (bad_player != null)
                PlayerList.Remove(bad_player);
            for (var i = 0; i < PlayersToDraw.Count; i++)
            {
                Player player = PlayersToDraw[i];
                player.Control(Game1.gameTime, 32, mapWidth, mapHeight, map.PCollisionLayer);
            }

            for (var i = 0; i < PlayersToDraw.Count; i++)
            {
                Player player = PlayersToDraw[i];
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
            scOverlay.Draw(spriteBatch);
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
                {
                    player.Respawn(Spawn.RandomVector(map), PlayersToDraw);
                    client.SendRespawn(player.Id);
                }
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
