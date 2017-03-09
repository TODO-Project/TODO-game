
using m_test1_hugo.Class.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using m_test1_hugo.Class.Main.Menus.pages;

namespace m_test1_hugo.Class.Bonuses
{
    public class Teleporter : Bonus
    {
        private bool isReady;
        private int coolDown = 5;
        private DateTime initChrono;
        const int Deplacement = 120;
        private int tempo = 50;
        private Teleporter link;
        public enum Side { Up, Right, Down, Left};

        private Side side;

        #region attr
        public bool IsReady
        {
            get
            {
                return isReady;
            }

            set
            {
                isReady = value;
            }
        }

        public int CoolDown
        {
            get
            {
                return coolDown;
            }

            set
            {
                coolDown = value;
            }
        }

        #endregion
        public Teleporter(Vector2 pos, Side side)
        {
            LoadContent(Game1.Content);
            name = "teleporter";
            this.side = side;
            this.Position = pos;
        }

        public override void interract(Player player)
        {
            if(IsReady)
            {
                IsReady = false;
                link.isReady = false;
                initChrono = DateTime.Now;
                link.initChrono = initChrono;
                player.Position = GetTpPlace(player);
            }
        }

        public void setLink(Teleporter teleporter)
        {
            teleporter.link = this;
            this.link = teleporter;
        }

        public Vector2 GetTpPlace(Player player)
        {
            Vector2 newPos = Vector2.Zero;
            switch (side)
            {
                case Side.Up:
                    newPos =  new Vector2(player.Position.X, player.Position.Y + Deplacement);
                    break;
                case Side.Down:
                    newPos = new Vector2(player.Position.X, player.Position.Y - Deplacement);
                    break;
                case Side.Left:
                    newPos = new Vector2(player.Position.X - Deplacement, player.Position.Y);
                    break;
                case Side.Right:
                    newPos = new Vector2(player.Position.X + Deplacement, player.Position.Y);
                    break;
            }
            return newPos;
        }

        public override void LoadContent(ContentManager content)
        {
            LoadContent(content, "Bonus/teleporter", 1, 2);
        }

        public override void Update(GameTime gametime)
        {
            currentColumn = isReady ? 0 : 1;

            if(!isReady)
            {
                if (DateTime.Now > initChrono.AddSeconds(CoolDown))
                {
                    link.isReady = true;
                    isReady = true;
                }
            }

            for (var i = 0; i < GamePage.PlayerList.Count; i++)
            {
                Player currentPlayer = GamePage.PlayerList[i];
                if (this.SpriteCollision(currentPlayer.destinationRectangle))
                {
                    interract(currentPlayer);
                }
            }

            Draw(Game1.spriteBatch);
        }
    }
}
