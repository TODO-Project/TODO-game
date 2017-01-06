using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Main.interfaces
{
    interface Collision
    {
        bool TileCollision(Sprite objet1);

        bool SpriteCollision(Sprite objet1, Sprite objet2); // objet 2 devra etre un rectangle pour utiliser Bounds.Intersects
    }
}
