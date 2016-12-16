using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Tile_Engine
{
    class Tile
    {
        #region Fields

        int tileIndex; // Numéro de la Tile dans le tileset
        int tileset;   // Numéro du tileset

        #endregion

        #region Properties

        public int TileIndex
        {
            get { return tileIndex; }
            set { tileIndex = value; }
        }

        public int Tileset
        {
            get { return tileset; }
            set { tileset = value; }
        }

        #endregion

        #region Constructor

        public Tile(int tileIndex, int tileset)
        {
            TileIndex = tileIndex;
            Tileset = tileset;
        }

        #endregion
    }
}
