using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Main.interfaces
{
    interface SpriteCollision
    {
        bool SpriteCollision(Rectangle objet1 ); // objet 2 devra etre un rectangle pour utiliser Bounds.Intersects
    }
}
