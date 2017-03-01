using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace m_test1_hugo.Class.Tile_Engine
{
    /// <summary>
    /// Définit un tileset, c'est à dire un liste de tous les tiles
    /// utilisables sur la carte de jeu, récupérée depuis une image.
    /// </summary>
    public class Tileset
    {
        #region Fields

        /// <summary>
        /// La texture du tileset
        /// </summary>
        private Texture2D image;

        /// <summary>
        /// La largeur des tiles, en pixels
        /// </summary>
        private int tileWidth;

        /// <summary>
        /// La hauteur des tiles, en pixels
        /// </summary>
        private int tileHeight;

        /// <summary>
        /// Le nombre de tiles en largeur
        /// </summary>
        private int tilesWide;

        /// <summary>
        /// Le nombre de tiles en hauteur
        /// </summary>
        private int tilesHigh;

        /// <summary>
        /// Tableau de rectangles qui décriront la zone de chaque tile
        /// </summary>
        private Rectangle[] sourceRectangles;

        #endregion

        #region Properties

        /// <summary>
        /// Récupère et définit la texture du tileset
        /// </summary>
        public Texture2D Image
        {
            get { return image; }
            set { image = value; }
        }

        /// <summary>
        /// Récupère et définit la largeur des tiles, en pixel
        /// </summary>
        public int TileWidth
        {
            get { return tileWidth; }
            set { tileWidth = value; }
        }

        /// <summary>
        /// Récupère et définit la hauteur des tiles, en pixels
        /// </summary>
        public int TileHeight
        {
            get { return tileHeight; }
            set { tileHeight = value; }
        }

        /// <summary>
        /// Récupère et définit le nombre de tiles en largeur
        /// </summary>
        public int TilesWide
        {
            get { return tilesWide; }
            set { tilesWide = value; }
        }

        /// <summary>
        /// Récupère et définit le nombre de tiles en hauteur
        /// </summary>
        public int TilesHigh
        {
            get { return tilesHigh; }
            set { tilesHigh = value; }
        }

        /// <summary>
        /// Récupère le tableau des rectangles sources pour chaque tileset
        /// </summary>
        public Rectangle[] SourceRectangles
        {
            get { return (Rectangle[])sourceRectangles.Clone(); } // Pour éviter de modifier les rectangles sources en mémoire
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Construit un tileset selon une image, la taille des tiles et sa taille
        /// </summary>
        /// <param name="image">La texture du tileset</param>
        /// <param name="tileWidth">La largeur des tiles en pixels</param>
        /// <param name="tileHeight">La hauteur des tiles en pixels</param>
        /// <param name="tilesWide">Le nombre de tiles en largeur</param>
        /// <param name="tilesHigh">Le nombre de tiles en hauteur</param>
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
