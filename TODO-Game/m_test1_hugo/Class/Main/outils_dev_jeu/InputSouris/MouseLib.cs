using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using m_test1_hugo.Class.Tile_Engine;

namespace m_test1_hugo.Class.Main.InputSouris
{
    public static class MouseLib
    {
        public static float posY // cote Oppose
        {
            get { return Camera.position.Y + Game1.ms.Y; }
        }

        public static float posX // cote Adjacent
        {
            get { return Camera.position.X + Game1.ms.X; }
        }
    }
}
  