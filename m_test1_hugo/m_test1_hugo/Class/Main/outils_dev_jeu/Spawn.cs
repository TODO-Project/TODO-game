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
            int mapWidth = map.mapLayers[0].Width * map.Tilesize;
            int mapHeight = map.mapLayers[0].Height * map.Tilesize;
            int tilesize = map.Tilesize;
            int x = rnd.Next(mapWidth);
            int y = rnd.Next(mapHeight);

            if (x <= tilesize)
                x += tilesize;
            else if (x >= mapWidth - tilesize)
                x -= tilesize;
            if (y <= tilesize)
                y += tilesize;
            else if (y >= mapHeight - tilesize)
                y -= tilesize;
            Console.WriteLine(x + "   " + y);

            while (map.PCollisionLayer.GetTile((x / map.Tilesize), (y / map.Tilesize)) == false)
            {
                Console.WriteLine("Spawn retry...");
                x = rnd.Next(map.mapLayers[0].Width);
                y = rnd.Next(map.mapLayers[0].Height);
                if (x <= tilesize)
                    x += tilesize;
                else if (x >= mapWidth - tilesize)
                    x -= tilesize;
                if (y <= tilesize)
                    y += tilesize;
                else if (y >= mapHeight - tilesize)
                    y -= tilesize;
                Console.WriteLine(x + "   " + y);
            }


            

            return new Vector2(x, y);
        }
    }
}
