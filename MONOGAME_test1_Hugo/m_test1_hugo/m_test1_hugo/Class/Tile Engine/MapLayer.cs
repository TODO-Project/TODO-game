using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Tile_Engine
{
    class MapLayer
    {
        #region Fields

        private Tile[,] map; // Tableau 2D pour stocker les tiles de la map

        #endregion

        #region Properties

        public int Width
        {
            get { return map.GetLength(1); }
        }

        public int Height
        {
            get { return map.GetLength(0); }
        }

        #endregion

        #region Constructors

        public MapLayer(Tile[,] map)
        {
            this.map = (Tile[,])map.Clone();      // Créee la map avec un tableau 2D de Tile préexistant
        }

        public MapLayer(int width, int height)
        {
            map = new Tile[height, width];

            for (int y = 0; y < height; y++)      //
            {                                     //
                for (int x = 0; x < width; x++)   //  Créee la map du jeu avec une nouvelle tile
                {                                 //
                    map[y, x] = new Tile(0, 0);   //
                }
            }
        }

        public MapLayer(string module)
        {
            using (var stream = new StreamReader("../../../../Content/" + module + ".txt")) // Ouverture du fichier texte qui contient le module
            {
                string line = stream.ReadToEnd();                // Lecture du fichier entier
                string[] values_string = line.Split(';');        // Split du fichier avec des ;
                var values = new int[values_string.Length];      // Création du tableau de valeurs
                for (int j = 0; j < values_string.Length; j++)   // Boucle pour transformer les strings en int
                {
                    values[j] = int.Parse(values_string[j]);
                }

                map = new Tile[values[0], values[1]];            // Deux premières valeurs du fichier : taille du module
                
                int i = 2;                                       

                for (int y = 0; y < values[1]; y++)              // Boucle pour assigner les tiles à la map
                {
                    for (int x = 0; x < values[0]; x++)
                    {
                        map[y, x] = new Tile(values[i], 0);
                        i++;
                    }
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Donne une tile de la map en prenant son x et son y
        /// </summary>
        /// <param name="x">Coordonnée x de la Tile</param>
        /// <param name="y">Coordonnée y de la Tile</param>
        /// <returns>Une instance de la classe Tile correspondant à celle indiquée par x et y</returns>
        public Tile getTile(int x, int y)
        {
            return map[y, x];
        }

        /// <summary>
        /// Change la tile aux coordonnées précisées
        /// </summary>
        /// <param name="x">Coordonnée x de la Tile</param>
        /// <param name="y">Coordonnée y de la Tile</param>
        /// <param name="tile">Tile à placer dans la map</param>
        public void setTile(int x, int y, Tile tile)
        {
            map[y, x] = tile;
        }


        /// <summary>
        /// Change la tile aux coordonnées précisées
        /// </summary>
        /// <param name="x">Coordonnée x de la Tile</param>
        /// <param name="y">Coordonnée y de la Tile</param>
        /// <param name="tileIndex">Numéro de la Tile dans le tileset</param>
        /// <param name="tileset">Numéro du tileset</param>
        public void setTile(int x, int y, int tileIndex, int tileset)
        {
            map[y, x] = new Tile(tileIndex, tileset);
        }

        #endregion
    }
}
