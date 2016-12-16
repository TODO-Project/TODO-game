using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace m_test1_hugo.Class.Tile_Engine
{
    class Tileset
    {
        #region Fields

        public Texture2D image;
        int tileWidth;   // In pixels
        int tileHeight;  // same
        int tilesWide;
        int tilesHigh;
        Rectangle[] sourceRectangles;

        #endregion

        #region Properties

        public Texture2D Image
        {
            get { return image; }
            set { image = value; }
        }

        public int TileWidth
        {
            get { return tileWidth; }
            set { tileWidth = value; }
        }

        public int TileHeight
        {
            get { return tileHeight; }
            set { tileHeight = value; }
        }

        public int TilesWide
        {
            get { return tilesWide; }
            set { tilesWide = value; }
        }

        public int TilesHigh
        {
            get { return tilesHigh; }
            set { tilesHigh = value; }
        }

        public Rectangle[] SourceRectangles
        {
            get { return (Rectangle[])sourceRectangles.Clone(); } // Pour éviter de modifier les rectangles sources en mémoire
        }

        #endregion

        #region Constructor

        public Tileset(Texture2D image, int tileWidth, int tileHeight, int tilesWide, int tilesHigh)
        {
            Image = image;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            TilesWide = tilesWide;
            TilesHigh = tilesHigh;

            int tiles = tilesWide * tilesHigh;

            sourceRectangles = new Rectangle[tiles];

            int tile = 0;

            for (int y = 0; y < tilesHigh; y++)
            {
                for (int x = 0; x < tilesWide; x++)
                {
                    sourceRectangles[tile] = new Rectangle(
                        x * tileWidth,
                        y * tileHeight,
                        tileWidth,
                        tileHeight
                        );
                    tile++;
                }
            }
        }

        #endregion
    }
}
