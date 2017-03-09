using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Tile_Engine
{
    /// <summary>
    /// Décrit une case dans la carte, utilisée pour construire la carte
    /// </summary>
    public class Tile
    {
        #region Fields

        /// <summary>
        /// Le numéro de la tile dans le tileset
        /// </summary>
        int tileIndex;

        /// <summary>
        /// Le numéro du tileset
        /// </summary>
        int tileset;

        #endregion

        #region Properties

        /// <summary>
        /// Récupère et définit le numéro de la tile dans le tileset
        /// </summary>
        public int TileIndex
        {
            get { return tileIndex; }
            set { tileIndex = value; }
        }

        /// <summary>
        /// Récupère et définit le numéro du tileset
        /// </summary>
        public int Tileset
        {
            get { return tileset; }
            set { tileset = value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Construit un tile selon son numéro de tile dans le tileset et le tileset utilisé
        /// </summary>
        /// <param name="tileIndex">Le numéro du tile dans le tileset</param>
        /// <param name="tileset">Le numéro du tileset</param>
        public Tile(int tileIndex, int tileset)
        {
            TileIndex = tileIndex;
            Tileset = tileset;
        }

        #endregion
    }
}
