using m_test1_hugo.Class.Main.interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Main
{
    public abstract class Bonus : AnimatedSprite, SpriteCollision
    {
        public abstract void interract(Player player);
        public static List<Bonus> BonusList = new List<Bonus>();
        public string name;

        public bool SpriteCollision(Rectangle player)
        {
            return destinationRectangle.Intersects(player);
        }

        public abstract void Update(GameTime gametime);
       
    }
}
