using m_test1_hugo.Class.Main.interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Main
{
    public abstract class Bonus : Sprite, SpriteCollision
    {
        public abstract void interract(Player player);
        public static List<Bonus> BonusList = new List<Bonus>();

        public bool SpriteCollision(Rectangle player)
        {
            return Bounds.Intersects(player);
        }

        public void Update(GameTime gametime)
        {
            for (var i = 0; i < Player.PlayerList.Count; i++)
            {
                Player currentPlayer = Player.PlayerList[i];

                if (this.SpriteCollision(currentPlayer.sourceRectangle))
                {
                    interract(currentPlayer);
                }
            }
        }
    }
}
