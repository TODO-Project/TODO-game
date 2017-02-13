using m_test1_hugo.Class.Tile_Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Main.outils_dev_jeu
{
    public static class Spawn
    {
        public static Vector2 RandomVector(int mapWidth, int mapHeight)
        {
            Random rnd = new Random();
            int x = rnd.Next(mapWidth);
            int y = rnd.Next(mapHeight);
            return new Vector2(x, y);
        }

        public static Vector2 RandomVector(TileMap map)
        {
            Random rnd = new Random();
            int x = rnd.Next(map.mapLayers[0].Width * map.Tilesize);
            int y = rnd.Next(map.mapLayers[0].Height * map.Tilesize);

            while (map.PCollisionLayer.GetTile((x / map.Tilesize), (y / map.Tilesize)) == false)
            {
                x = rnd.Next(map.mapLayers[0].Width);
                y = rnd.Next(map.mapLayers[0].Height);
            }

            //Console.WriteLine(x + "   " + y);

            return new Vector2(x, y);
        }
    }
}
