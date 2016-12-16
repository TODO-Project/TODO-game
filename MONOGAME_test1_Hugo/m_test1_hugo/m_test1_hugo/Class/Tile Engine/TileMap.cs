using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using m_test1_hugo.Class.Main;

namespace m_test1_hugo.Class.Tile_Engine
{
    class TileMap
    {
        #region Fields

        List<Tileset> tilesets;
        List<MapLayer> mapLayers;

        #endregion

        #region Properties

        #endregion

        #region Constructors

        public TileMap(List<Tileset> tilesets, List<MapLayer> mapLayers)
        {
            this.tilesets = tilesets;
            this.mapLayers = mapLayers;
        }

        public TileMap(Tileset tileset, MapLayer mapLayer)
        {
            tilesets = new List<Tileset>();
            tilesets.Add(tileset);

            mapLayers = new List<MapLayer>();
            mapLayers.Add(mapLayer);
        }

        #endregion

        #region Methods

        public void Draw(SpriteBatch spritebatch)
        {
            Rectangle destination = new Rectangle(0, 0, TileEngine.TileWidth, TileEngine.TileHeight);
            Tile tile;

            foreach (MapLayer layer in mapLayers)
            {
                for (int y = 0; y < layer.Height; y++)
                {

                    destination.Y = y * TileEngine.TileHeight;
                    for (int x = 0; x < layer.Width; x++)
                    {
                        tile = layer.getTile(x, y);

                        destination.X = x * TileEngine.TileWidth;

                        spritebatch.Draw(
                            tilesets[tile.Tileset].Image,
                            destination,
                            tilesets[tile.Tileset].SourceRectangles[tile.TileIndex],
                            Color.White
                            );
                    }
                }
            }
        }

        #endregion
    }
}
