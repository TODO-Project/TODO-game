using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace m_test1_hugo.Class.Main
{
    /// <summary>
    /// Décrit le moteur graphique du jeu
    /// </summary>
    class TileEngine
    {
        #region Fields

        /// <summary>
        /// La largeur des tiles, en pixels
        /// </summary>
        private static int tileWidth;

        /// <summary>
        /// La hauteur des tiles, en pixels
        /// </summary>
        private static int tileHeight;

        #endregion

        #region Properties

        /// <summary>
        /// Récupère et définit la largeur des tiles en pixels
        /// </summary>
        public static int TileWidth
        {
            get { return tileWidth; }
            set { tileWidth = value; }
        }

        /// <summary>
        /// Récupère et définit la largeur des tiles en pixels
        /// </summary>
        public static int TileHeight
        {
            get { return tileHeight; }
            set { tileHeight = value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Construit un moteur graphique selon la largeur et la hauteur des tiles
        /// </summary>
        /// <param name="tileWidth">La largeur des tiles en pixels</param>
        /// <param name="tileHeight">La hauteur des tiles en pixels</param>
        public TileEngine(int tileWidth, int tileHeight)
        {
            TileEngine.tileWidth = tileWidth;
            TileEngine.tileHeight = tileHeight;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Récupère la position des tiles sur la map
        /// </summary>
        /// <param name="position">La position des tiles</param>
        /// <returns>Un point relatif au vector2</returns>
        public Point VectorToCell(Vector2 position)
        {
            return new Point((int)position.X / tileWidth, (int)position.Y / tileHeight);
        }

        #endregion
    }
}
