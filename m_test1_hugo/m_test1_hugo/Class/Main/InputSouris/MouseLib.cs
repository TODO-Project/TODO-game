using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Main.InputSouris
{
    public static class MouseLib
    {
        public static float posY // cote Oppose
        {
            get { return Game1.ms.Y; }
        }

        public static float posX // cote Adjacent
        {
            get { return Game1.ms.X; }
        }
    }
}
  