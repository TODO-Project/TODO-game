using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class
{
    
    class CollisionLayer
    {
        #region Fields

        bool[,] map;

        #endregion

        #region Properties

        #endregion

        #region Constructors

        public CollisionLayer(int height, int width)
        {
            map = new bool[height, width];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    SetTile(x, y, true);
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Récupère la valeur de collision d'une tile de la couche de collision
        /// </summary>
        /// <param name="x">Coordonnée x de la tile</param>
        /// <param name="y">Coordonnée y de la tile</param>
        /// <returns>Une valeur CollisionType (true or false)</returns>
        public bool GetTile(int x, int y)
        {
            return map[y, x];
        }

        /// <summary>
        /// Met une tile de la couche de collision à une valeur
        /// </summary>
        /// <param name="x">Coordonnée x de la tile</param>
        /// <param name="y">Coordonnée y de la tile</param>
        /// <param name="value">Valeur de la collision (true ou false)</param>
        public void SetTile(int x, int y, bool value)
        {
            map[y, x] = value;
        }

        #endregion
    }
}
