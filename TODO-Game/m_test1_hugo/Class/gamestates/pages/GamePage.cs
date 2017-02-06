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
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

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

        #region teams
        Team TeamBlue;
        Team TeamRed;
        #endregion

        #region Players
        public static string Pseudo;
        public static Player player;
        public static List<Player> PlayerList;
        #endregion

        #region bonuses
        public static List<Bonus> BonusList;
        public static List<Pics> PicList = new List<Pics>();
        #endregion

        #endregion

        public GamePage()
        {
            PlayerList = new List<Player>();
            BonusList = new List<Bonus>();
            font = Game1.Content.Load<SpriteFont>("font");
            Console.WriteLine("gamepage created");
            // TODO: Add your initialization logic heres
            #region teams intialization
            TeamBlue = new Team(1, "blue");
            TeamRed = new Team(2, "red");
            #endregion

            #region Drawing Map
            Texture2D tilesetTexture = Game1.Content.Load<Texture2D>("terrain");
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
            maps.Add("maps/hugo/1");
            maps.Add("maps/grassy32/1");
            maps.Add("maps/grassy32/1");

            List<string> maps2 = new List<string>();
            maps2.Add("maps/grassy32/2");
            maps2.Add("maps/hugo/2");
            maps2.Add("maps/grassy32/2");
            maps2.Add("maps/grassy32/2");

            List<string> ponts1 = new List<string>();
            ponts1.Add("maps/pont/bas");
            ponts1.Add("maps/pont/bas");
            ponts1.Add("maps/pont/haut");
            ponts1.Add("maps/pont/haut");

            List<string> ponts2 = new List<string>();
            ponts2.Add("maps/pont/droite");
            ponts2.Add("maps/pont/gauche");
            ponts2.Add("maps/pont/droite");
            ponts2.Add("maps/pont/gauche");

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
            */

            MapLayer layer = new MapLayer(maps, 32, ordre);
            MapLayer layer2 = new MapLayer(maps2, 32, ordre);
            MapLayer pont1 = new MapLayer(ponts1, 32, ordreNormal);
            MapLayer pont2 = new MapLayer(ponts2, 32, ordreNormal);

            var layers = new List<MapLayer>();
            layers.Add(layer);
            layers.Add(layer2);
            layers.Add(pont1);
            layers.Add(pont2);

            var tilesets = new List<Tileset>();
            tilesets.Add(tileset);

            map = new TileMap(tilesets, layers);
            map.Tilesize = tilesize;
            mapWidth = map.GetWidth();
            mapHeight = map.GetHeight();
            #endregion

            TeamBlue._Color = Color.Blue;
            TeamRed._Color = Color.Red;
            TeamBlue.TeamKills = 0;
            TeamRed.TeamKills = 0;

            foreach (Player player in PlayerList)
            {
                player.Kills = 0;
                player.Deaths = 0;
            }
                

            new Player(Pseudo, new Sprinter(), new M16(), TeamBlue, azerty, Spawn.RandomVector(50,50));
            player = PlayerList[0];
            player.Health = 20;

            //SpeedBuff speedBuff = new SpeedBuff();

            Heal heal = new Heal();
            heal.Position = new Vector2(150, 150);


            new MagicBox(Spawn.RandomVector(500, 500));

            overlay = new Overlay(player);

            camera = new Camera(Game1.graphics.GraphicsDevice.Viewport);

            new Player(new Sprinter(), new Assault(), TeamRed, gamepad, Spawn.RandomVector(800,800));

            //overlay.LoadContent(Content);
        }

        public override MenuPage Action()
        {
            Update();


            if(TeamBlue.TeamKills >= 5)
            {
                return new MainPage();
            }
            else
            {
                return null;
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.End();

            var viewMatrix = camera.GetViewMatrix();

            spriteBatch.Begin(transformMatrix: viewMatrix);
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

            #region Drawing and updating players
            for (var i = 0; i < PlayerList.Count; i++)
            {
                Player player = PlayerList[i];
                player.healthBar.Draw(spriteBatch);
                player.UpdateSprite(Game1.gameTime);
                player.Control(Game1.gameTime, 32, mapWidth, mapHeight, map.PCollisionLayer);
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
            spriteBatch.End();
        }

        public override void Update()
        {
            //overlay.Update(Game1.gameTime);
            ms = Mouse.GetState();
            kb = Keyboard.GetState();

            TeamBlue.Update();
            TeamRed.Update();

            if (kb.IsKeyDown(Keys.P))
            {
                new ClothBox(Spawn.RandomVector(500, 500));
                //player.weapon = new shotgun(player);
            }

            if (kb.IsKeyDown(Keys.I))
            {
                new MagicBox(Spawn.RandomVector(500, 500));
                //player.weapon = new shotgun(player);
            }

            if (kb.IsKeyDown(Keys.O))
                new Player(new Sprinter(), new Assault(), TeamRed, gamepad, Spawn.RandomVector(900, 900));
        }

        public void DrawTeamScores(SpriteBatch spriteBatch)
        {
            string blueScore = TeamBlue.TeamKills.ToString();
            string redScore = TeamRed.TeamKills.ToString();
            spriteBatch.DrawString(font, blueScore, BlueScorePosition, TeamBlue._Color, 0f, font.MeasureString(blueScore), 0.6f, SpriteEffects.None, 0.3f ); //drawing blue score
            spriteBatch.DrawString(font, ":", new Vector2(BlueScorePosition.X + 30, 100), Color.White, 0f, font.MeasureString(":") / 2, 0.6f, SpriteEffects.None, 0.3f); 
            spriteBatch.DrawString(font, redScore, RedScorePosition, TeamRed._Color, 0f, font.MeasureString(redScore), 0.6f, SpriteEffects.None, 0.3f); //drawing red score
        }
    }
}
