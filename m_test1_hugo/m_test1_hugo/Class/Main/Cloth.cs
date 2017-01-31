using m_test1_hugo.Class.clothes;
using m_test1_hugo.Class.Main;
using Microsoft.Xna.Framework;
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
            new Shirt ("batMan", 50),
            new Shirt("superMan", 25),
            new Pant("superMan", 12),
            new Pant("batMan", 5),
            new Boots("batMan", 2),
            new Boots("superMan", 3)
        };

        //public abstract void Update(GameTime gameTime);
        public abstract void interract(Player player);

        public string clothName;

        public abstract int Bonus
        {
            get;
            set;
        }

        public Cloth(string name, int bonus)
        {
            this.clothName = name;
            this.Bonus = bonus;
        }

        public abstract void TakeOff(Player player);

    }
}
