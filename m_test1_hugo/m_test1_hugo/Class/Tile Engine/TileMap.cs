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

        List<Tileset> tilesets;   // Liste des tilesets utilisés sur la map
        List<MapLayer> mapLayers; // Liste des couches de la map
        CollisionLayer collisionLayer; // Couche de collision

        #endregion

        #region Properties

        #endregion

        #region Constructors

        public TileMap(List<Tileset> tilesets, List<MapLayer> mapLayers)  // Construit la map selon des liste préexistantes
        {
            // Ajout des tilesets
            this.tilesets = tilesets;

            // Ajout des maplayers
            this.mapLayers = mapLayers;

            // Ajout de la collision
            collisionLayer = new CollisionLayer(mapLayers[0].Height, mapLayers[0].Width);
            foreach (MapLayer maplayer in mapLayers)
            {
                ProcessColisionLayer(maplayer);
            }
        }

        public TileMap(Tileset tileset, MapLayer mapLayer)  // Créee la map avec un tileset et une maplayer
        {
            // Ajout du tileset
            tilesets = new List<Tileset>();
            tilesets.Add(tileset);

            // Ajout des maplayers
            mapLayers = new List<MapLayer>();
            mapLayers.Add(mapLayer);

            // Ajout de la collision
            collisionLayer = new CollisionLayer(mapLayers[0].Height, mapLayers[0].Width);
            foreach (MapLayer maplayer in mapLayers)
            {
                ProcessColisionLayer(maplayer);
            }


        }

        #endregion

        #region Methods


        /// <summary>
        /// Dessine la map à l'écrab
        /// </summary>
        /// <param name="spritebatch">Spritebatch utilisé pour dessiner</param>
        public void Draw(SpriteBatch spritebatch)
        {
            Rectangle destination = new Rectangle(0, 0, TileEngine.TileWidth, TileEngine.TileHeight); // Utiliser un seul rectangle pour optimiser les calculs ; seuls les coordonnées changent
            Tile tile; // Utiliser une seule tile pour optimiser les calculs

            foreach (MapLayer layer in mapLayers) // Possibilité de superposer des maps (meilleurs effets graphiques)
            {
                for (int y = 0; y < layer.Height; y++)
                {

                    destination.Y = y * TileEngine.TileHeight; // Calcul de la position Y du rectangle de destination
                    for (int x = 0; x < layer.Width; x++)
                    {
                        tile = layer.getTile(x, y); // Récupération de la tile en cours de traitement

                        destination.X = x * TileEngine.TileWidth; // Calcul de la position X du rectangle de destination

                        spritebatch.Draw( // Dessin de la map
                            tilesets[tile.Tileset].Image, // Texture du tileset
                            destination, // Rectangle de destination
                            tilesets[tile.Tileset].SourceRectangles[tile.TileIndex], // Rectangle source de la Tile
                            Color.White // Pas d'effet coloré
                            );
                    }
                }
            }
        }

        private void ProcessColisionLayer(MapLayer layer)
        {
            for (int y = 0; y < layer.Height; y++)
            {
                for (int x = 0; x < layer.Width; x++)
                {
                    switch(layer.getTile(x,y).TileIndex)
                    {
                        case 955:
                        case 956:
                        case 987:
                        case 988:
                        case 1019:
                        case 1020:
                            collisionLayer.SetTile(x, y, false);
                            break;
                    }
                }
            }
        }

        #endregion
    }
}
