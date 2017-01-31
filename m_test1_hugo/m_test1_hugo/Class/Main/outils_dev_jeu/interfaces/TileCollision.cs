using m_test1_hugo.Class.Tile_Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Main.interfaces
{
    interface TileCollision
    {
        bool TileCollision(Sprite objet1, int tileSize, int mapWidth, int mapHeight, CollisionLayer collisionLayer, int direction);

        
    }
}
