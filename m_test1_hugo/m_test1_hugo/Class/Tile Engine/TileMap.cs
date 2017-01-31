using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using m_test1_hugo.Class.Main;
using System.IO;

namespace m_test1_hugo.Class.Tile_Engine
{
    public class TileMap
    {
        #region Fields

        public List<Tileset> tilesets;   // Liste des tilesets utilisés sur la map
        public List<MapLayer> mapLayers; // Liste des couches de la map
        public CollisionLayer collisionLayer; // Couche de collision
        public CollisionLayer bulletCollisionLayer; // Couche de collision avec les balles
        private int tilesize;

        #endregion

        #region Properties

        public CollisionLayer PCollisionLayer
        {
            get { return collisionLayer; }
        }

        public CollisionLayer BCollisionLayer
        {
            get { return bulletCollisionLayer; }
        }

        public int Tilesize
        {
            get { return tilesize; }
            set { tilesize = value; }
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
            bulletCollisionLayer = new CollisionLayer(mapLayers[0].Height, mapLayers[0].Width);
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
            bulletCollisionLayer = new CollisionLayer(mapLayers[0].Height, mapLayers[0].Width);
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
        /// Charge les valeurs de collisions des blocs
        /// </summary>
        /// <param name="fileName">Le nom du fichier</param>
        /// <returns>Les valeurs de collision</returns>
        public int[] loadCollisionFile(string fileName)
        {
            string line;
            string[] lines;
            int[] res;
            Console.WriteLine("../../../../Content/collisions/" + fileName + ".txt");
            using (var stream = new StreamReader("../../../../Content/collisions/" + fileName +".txt"))
            {
                line = stream.ReadToEnd();
                lines = line.Split(';');
                res = new int[lines.Length];

                for (int i = 0; i < lines.Length; i++)
                {
                    res[i] = int.Parse(lines[i]);
                }
            }

            return res;
        }

        /// <summary>
        /// Traite la collision
        /// </summary>
        /// <param name="layer">Couche de la map</param>
        private void ProcessColisionLayer(MapLayer layer)
        {
            int[] bulletCollisionValues;
            int[] spriteCollisionValues;
            int[] bridgeCollisionValues;

            bulletCollisionValues = loadCollisionFile("bulletcollision");
            spriteCollisionValues = loadCollisionFile("spritecollision");
            bridgeCollisionValues = loadCollisionFile("bridgecollision");

            for (int y = 0; y < layer.Height; y++)
            {
                for (int x = 0; x < layer.Width; x++)
                {
                    // Balles
                    if (bulletCollisionValues.Contains(layer.getTile(x, y).TileIndex))
                        bulletCollisionLayer.SetTile(x, y, false);

                    if (spriteCollisionValues.Contains(layer.getTile(x, y).TileIndex))
                        collisionLayer.SetTile(x, y, false);
                    
                    if (bridgeCollisionValues.Contains(layer.getTile(x, y).TileIndex))
                    {
                        bulletCollisionLayer.SetTile(x, y, true);
                        collisionLayer.SetTile(x, y, true);
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
