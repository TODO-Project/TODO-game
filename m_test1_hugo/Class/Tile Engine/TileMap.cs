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
    /// <summary>
    /// Décrit la carte du jeu
    /// </summary>
    public class TileMap
    {
        #region Fields

        private static string path = Game1.IsRelease ? "Content/collisions/" : "../../../../Content/collisions/";

        /// <summary>
        /// Liste des tilesets utilisés dans la carte
        /// </summary>
        private List<Tileset> tilesets;

        /// <summary>
        /// Liste des couches de la carte
        /// </summary>
        private List<MapLayer> mapLayers;

        /// <summary>
        /// La couche de collision de la carte
        /// </summary>
        private CollisionLayer collisionLayer;

        /// <summary>
        /// La couche de collision des balles
        /// </summary>
        private CollisionLayer bulletCollisionLayer;

        /// <summary>
        /// La taille des tiles
        /// </summary>
        private int tilesize;

        #endregion

        #region Properties

        /// <summary>
        /// Récupère la couche de collision entre le joueur et la carte
        /// </summary>
        public CollisionLayer PCollisionLayer
        {
            get { return collisionLayer; }
        }

        /// <summary>
        /// Récupère la couche de collision entre les balles et la carte
        /// </summary>
        public CollisionLayer BCollisionLayer
        {
            get { return bulletCollisionLayer; }
        }

        /// <summary>
        /// Récupère et définit la taille des tiles de la carte
        /// </summary>
        public int Tilesize
        {
            get { return tilesize; }
            set { tilesize = value; }
        }

        /// <summary>
        /// Récupère et définit la liste des couches de la map
        /// </summary>
        public List<MapLayer> MapLayers
        {
            get
            {
                return mapLayers;
            }

            set
            {
                mapLayers = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Construit une carte de jeu selon une liste de tilesets et une liste de couches de carte
        /// </summary>
        /// <param name="tilesets"></param>
        /// <param name="mapLayers"></param>
        public TileMap(List<Tileset> tilesets, List<MapLayer> mapLayers)
        {
            // Ajout des tilesets
            this.tilesets = tilesets;

            // Ajout des maplayers
            this.MapLayers = mapLayers;

            // Ajout de la collision
            collisionLayer = new CollisionLayer(mapLayers[0].Height, mapLayers[0].Width);
            bulletCollisionLayer = new CollisionLayer(mapLayers[0].Height, mapLayers[0].Width);
            foreach (MapLayer maplayer in mapLayers)
            {
                ProcessColisionLayer(maplayer);
            }
        }

        /// <summary>
        /// Construit une carte de jeu avec un tileset et une carte de jeu
        /// </summary>
        /// <param name="tileset"></param>
        /// <param name="mapLayer"></param>
        public TileMap(Tileset tileset, MapLayer mapLayer)
        {
            // Ajout du tileset
            tilesets = new List<Tileset>();
            tilesets.Add(tileset);

            // Ajout des maplayers
            MapLayers = new List<MapLayer>();
            MapLayers.Add(mapLayer);

            // Ajout de la collision
            collisionLayer = new CollisionLayer(MapLayers[0].Height, MapLayers[0].Width);
            bulletCollisionLayer = new CollisionLayer(MapLayers[0].Height, MapLayers[0].Width);
            foreach (MapLayer maplayer in MapLayers)
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

            foreach (MapLayer layer in MapLayers) // Possibilité de superposer des maps (meilleurs effets graphiques)
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
            //Console.WriteLine("../../../../Content/collisions/" + fileName + ".txt");
            using (var stream = new StreamReader(path + fileName + ".txt"))
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

        /// <summary>
        /// Récupère la largeur de la carte
        /// </summary>
        /// <returns></returns>
        public int GetWidth()
        {
            return MapLayers[0].Width;
        }

        /// <summary>
        /// Récupère la hauteur de la carte
        /// </summary>
        /// <returns></returns>
        public int GetHeight()
        {
            return MapLayers[0].Height;
        }

        #endregion
    }
}
