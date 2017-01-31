using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace m_test1_hugo.Class.Tile_Engine
{
    public class Tileset
    {
        #region Fields

        public Texture2D image;          // Texture du tileset
        int tileWidth;                   // En pixels
        int tileHeight;                  // En pixels
        int tilesWide;                   // Nombre de tiles en largeur
        int tilesHigh;                   // Nombre de tiles en hauteur
        Rectangle[] sourceRectangles;    // Tableau de Rectangles qui recouvriront chacun un tile

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

            int tiles = tilesWide * tilesHigh; // Nombre de tiles au total

            sourceRectangles = new Rectangle[tiles]; // Création de la table des rectangles avec le bon nombre de rectangles

            int tile = 0; // Numéro de la tile dans la table des rectangles

            for (int y = 0; y < tilesHigh; y++)
            {
                for (int x = 0; x < tilesWide; x++)
                {
                    sourceRectangles[tile] = new Rectangle(  // Création de chaque rectangle source des tiles
                        x * tileWidth,   // Coordonnée x
                        y * tileHeight,  // Coordonéée y
                        tileWidth,       // Largeur
                        tileHeight       // Hauteur
                        );
                    tile++;
                }
            }
        }

        #endregion
    }
}
