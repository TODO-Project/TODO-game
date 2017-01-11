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

        public CollisionLayer PCollisionLayer
        {
            get { return collisionLayer; }
        }

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

        /// <summary>
        /// Traite la collision
        /// </summary>
        /// <param name="layer">Couche de la map</param>
        private void ProcessColisionLayer(MapLayer layer)
        {
            for (int y = 0; y < layer.Height; y++)
            {
                for (int x = 0; x < layer.Width; x++)
                {
                    // Collisions solides
                    switch(layer.getTile(x,y).TileIndex)
                    {
                        // BORDURES DE MAP
                        // Rocky
                        case 005:
                        case 006:
                        case 007:
                        case 037:
                        case 039:
                        case 069:
                        case 070:
                        case 071:
                        // Lava
                        case 274:
                        case 275:
                        case 276:
                        case 306:
                        case 308:
                        case 338:
                        case 339:
                        case 340:
                        // Start
                        case 085:
                        case 086:
                        case 087:
                        case 117:
                        case 119:
                        case 149:
                        case 150:
                        case 151:
                        // Beach
                        case 352:
                        case 353:
                        case 354:
                        case 384:
                        case 386:
                        case 416:
                        case 417:
                        case 418:
                        // EAU
                        case 358:
                        case 359:
                        case 360:
                        case 422:
                        case 423:
                        case 424:
                        /////////
                        case 292:
                        case 420:
                        case 293:
                        case 387:
                        case 419:
                        case 325:
                        case 356:
                        case 324:
                        case 389:
                        case 391:
                        // LAVE
                        case 271:
                        case 272:
                        case 273:
                        case 303:
                        case 304:
                        case 305:
                        case 335:
                        case 336:
                        case 337:
                        // ROCHER
                        case 763:
                        case 764:
                        case 795:
                        case 796:
                        // TOMBE
                        case 687:
                        case 719:
                        // TROU TERRE CLAIRE
                        case 088:
                        case 089:
                        case 090:
                        case 120:
                        case 121:
                        case 122:
                        case 152:
                        case 153:
                        case 154:
                        // STATUE MOAI
                        case 399:
                        case 400:
                        case 431:
                        case 432:
                        case 463:
                        case 464:
                        // ARBRE
                        case 955:
                        case 956:
                        case 987:
                        case 988:
                        case 1019:
                        case 1020:
                        ////////
                        case 0030:
                        case 0031:
                        case 0062:
                        case 0063:
                        case 0094:
                        case 0095:
                        case 0126:
                        case 0127:
                        case 0158:
                        case 0159:
                            collisionLayer.SetTile(x, y, false);
                            break;
                    }

                    // Exception des ponts
                    switch(layer.getTile(x,y).TileIndex)
                    {
                        // DROITE
                        case 525:
                        case 557:

                        // GAUCHE
                        case 527:
                        case 559:

                        // HAUT
                        case 654:
                        
                        // BAS
                        case 590:
                        case 622:

                        // Pont suspendu
                        case 591:
                        case 623:
                        case 655:
                            collisionLayer.SetTile(x, y, true);
                            break;
                    }
                }
            }
        }

        public int GetWidth()
        {
            return mapLayers[0].Width;
        }

        public int GetHeight()
        {
            return mapLayers[0].Height;
        }

        #endregion
    }
}
