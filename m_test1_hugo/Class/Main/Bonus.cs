using m_test1_hugo.Class.Main.interfaces;
using m_test1_hugo.Class.Main.Menus.pages;
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

        public Bonus()
        {
            GamePage.BonusList.Add(this);
        }
        /// <summary>
        /// methode qui va faire interragir le joueur avec le bonus
        /// </summary>
        /// <param name="player"></param>
        public abstract void interract(Player player);
        public string name;

        /// <summary>
        /// methode qui recupere la collision du bonus avec un joueur
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public bool SpriteCollision(Rectangle player)
        {
            return destinationRectangle.Intersects(player);
        }

        public abstract void Update(GameTime gametime);
       
    }
}
