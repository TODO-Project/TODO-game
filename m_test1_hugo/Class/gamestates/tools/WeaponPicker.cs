using m_test1_hugo.Class.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using m_test1_hugo.Class.Main.outils_dev_jeu.ArmesVignette;
using Microsoft.Xna.Framework.Input;
using m_test1_hugo.Class.Characters.Teams;
using m_test1_hugo.Class.Characters;
using m_test1_hugo.Class.Main.outils_dev_jeu.ControlLayouts;
using m_test1_hugo.Class.Weapons;
using m_test1_hugo.Class.ControlLayouts;

namespace m_test1_hugo.Class.gamestates.tools
{
    class WeaponPicker : Sprite
    {
        private List<Player> Players = new List<Player>();
        private Weapon[] WeaponList = Weapon.List;
        private WeaponPic ActivePic;
        public int TeamCount = 0, WeaponCount = 0;
        private SpriteFont font;
        public Vector2 PicPosition, RightArrowPos, LeftArrowPos, WeaponTextPosition;
        public bool wHold, tHold; // weaponHold, TeamHold
        private Color ActiveColor;
        private Player ActivePlayer;
        public Team ActiveTeam;
        public static float CO;

        Texture2D LeftArrow, RightArrow, UpArrow, DownArrow;
        
        #region properties

        public Vector2 UpArrowPos
        {
            get
            {
                return new Vector2(Position.X, Position.Y - 250);
            }
        }

        public Vector2 DownArrowPos
        {
            get
            {
                return new Vector2(Position.X, Position.Y + 250);
            }
        }
        #endregion

        public WeaponPicker(Vector2 pos)
        {
            Position = pos;
            LoadContent(Game1.Content);
        }

        public override void LoadContent(ContentManager content)
        {
            #region teams
            Team TeamBlue = new Team(1, "blue", Color.Blue);
            Team TeamRed = new Team(2, "red", Color.Red);
            #endregion
            font = content.Load<SpriteFont>("font");
            texture = content.Load<Texture2D>("menu/textarea");
            #region arrows
            UpArrow = content.Load<Texture2D>("menu/UpArrow");
            DownArrow = content.Load<Texture2D>("menu/DownArrow");
            LeftArrow = content.Load<Texture2D>("menu/LeftArrow");
            RightArrow = content.Load<Texture2D>("menu/RightArrow");
            #endregion
            ActivePic = new WeaponPic(WeaponList[0], Center);
            PicPosition = new Vector2(Position.X, 50/2);
            RightArrowPos = new Vector2(Position.X+150, Position.Y);
            LeftArrowPos = new Vector2(PicPosition.X - 150, RightArrowPos.Y);
            WeaponTextPosition = new Vector2(PicPosition.X+20, Center.Y-80);
            #region players
            Players.Add(new Player(new Sprinter(), null, Team.TeamList[0], new Azerty(), Position));
            Players.Add(new Player(new Sprinter(), null, Team.TeamList[1], new Azerty(), Position));
            Players[0].Pseudo = "";
            Players[1].Pseudo = "";
            Players[0].LoadContent(content);
            Players[1].LoadContent(content);
            #endregion
            ActiveColor = Color.Blue;
            ActivePlayer = Players[0];
            /*if(WeaponList[WeaponCount] is Glock)
            ActivePlayer.weapon = WeaponList[WeaponCount];*/
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, WeaponList[WeaponCount].Name, WeaponTextPosition, Color.White, 0f, font.MeasureString(WeaponList[WeaponCount].Name)/2, 0.3f, SpriteEffects.None, 1f);

            if (WeaponCount < WeaponList.Count() - 1)
                spriteBatch.Draw(RightArrow, RightArrowPos, Color.White);

            if(WeaponCount > 0)
                spriteBatch.Draw(LeftArrow, LeftArrowPos, Color.White);

            spriteBatch.Draw(UpArrow, UpArrowPos, ActiveColor);
            spriteBatch.Draw(DownArrow, DownArrowPos, ActiveColor);
            ActivePlayer.isMoving = true;
            ActivePlayer.Draw(spriteBatch);


            var CursY = Mouse.GetState().Position.Y;
            var CursX = Mouse.GetState().Position.X;
            var CA = -(ActivePlayer.Center.Y - CursY);
            CO = -(ActivePlayer.Center.X - CursX);
            var MouseRotationAngle = (float)(Math.Atan(CA / CO));
            ActivePlayer.MouseRotationAngle = MouseRotationAngle;

            if (ActivePlayer.weapon != null)
            {
                if (CO <= 0)// cote oppose holder ( voir dans les attributs, et faire un schema si besoin)
                {
                    spriteBatch.Draw(ActivePic.texture, ActivePlayer.Center, null, Color.White, MouseRotationAngle, new Vector2(0, ActivePlayer.weapon.Height / 2), -1.0f, SpriteEffects.FlipVertically, 0f); // mettre en comm pour tester ce que ca fait 
                }
                else
                {
                    spriteBatch.Draw(ActivePic.texture, ActivePlayer.Center, null, Color.White, MouseRotationAngle, new Vector2(0, ActivePlayer.weapon.Height / 2), 1.0f, SpriteEffects.None, 0f);
                }
            }
        }

        public void Update()
        {
            Draw(Game1.spriteBatch);
            KeyboardState kb = Keyboard.GetState();
            if (kb.IsKeyDown(Keys.Right) && WeaponCount < WeaponList.Count()-1 && !wHold)
            {
                wHold = true;
                WeaponCount++;
                ActivePic = new WeaponPic(WeaponList[WeaponCount], Center);
                ActivePlayer.weapon = WeaponList[WeaponCount];
                //draw la fleche
            }

            else if (kb.IsKeyDown(Keys.Left) && WeaponCount > 0 && !wHold)
            {
                wHold = true;
                WeaponCount--;
                ActivePic = new WeaponPic(WeaponList[WeaponCount], Center);
                ActivePlayer.weapon = WeaponList[WeaponCount];
                //draw la fleche
            }
            if (kb.IsKeyUp(Keys.Left) && kb.IsKeyUp(Keys.Right))
                wHold = false;
            #region teamPicker
            if (kb.IsKeyDown(Keys.Up) && !tHold)
            {
                tHold = true;
                TeamCount++;
            }
            else if (kb.IsKeyDown(Keys.Down) && !tHold)
            {
                tHold = true;
                TeamCount--;
            }
            else if (kb.IsKeyUp(Keys.Down) && kb.IsKeyUp(Keys.Up))
            {
                tHold = false;
            }
            int index = TeamCount % 2 == 0 ? 0 : 1;
            ActiveColor = TeamCount % 2 == 0 ? Color.Blue : Color.Red;
            ActivePlayer = TeamCount % 2 == 0 ? Players[0] : Players[1];
            ActiveTeam = Team.TeamList[index];
            ActivePlayer.weapon = WeaponList[WeaponCount];
            ActivePlayer.weapon.Holder = ActivePlayer;
            /*if (ActivePlayer.weapon != null)
                ActivePlayer.;*/
            ActivePlayer.UpdateSprite(Game1.gameTime);
            ActivePlayer.Control(Game1.gameTime);

            Draw(Game1.spriteBatch);
            #endregion
        }
    }
}