using m_test1_hugo.Class.clothes;
using m_test1_hugo.Class.Main;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrainementProjet1.Class.Main
{
    public abstract class Cloth : Sprite
    {
        public static Cloth[] list = new Cloth[]
        {
            //Cloth leatherShirt = 
            //

                // demander comment inserer != types de vetements

            //
        };

        public int speedBonus;
        public int healthBonus;
        public string name;

        public abstract void interract(Player player);

        public Player holder;

    }
}
