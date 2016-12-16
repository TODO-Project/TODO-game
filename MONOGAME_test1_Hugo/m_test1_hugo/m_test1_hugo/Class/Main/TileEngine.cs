using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace m_test1_hugo.Class.Main
{
    class TileEngine
    {
        #region Fields

        private static int tileWidth;
        private static int tileHeight;

        #endregion

        #region Properties

        public static int TileWidth
        {
            get { return tileWidth; }
            set { tileWidth = value; }
        }

        public static int TileHeight
        {
            get { return tileHeight; }
            set { tileHeight = value; }
        }

        #endregion

        #region Constructor

        public TileEngine(int tileWidth, int tileHeight)
        {
            TileEngine.tileWidth = tileWidth;
            TileEngine.tileHeight = tileHeight;
        }

        #endregion

        #region Methods

        public Point VectorToCell(Vector2 position)
        {
            return new Point((int)position.X / tileWidth, (int)position.Y / tileHeight);
        }

        #endregion
    }
}
