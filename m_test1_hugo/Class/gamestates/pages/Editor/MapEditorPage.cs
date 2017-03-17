using m_test1_hugo.Class.Main.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using m_test1_hugo.Class.Tile_Engine;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace m_test1_hugo.Class.gamestates.pages.Editor
{
    class MapEditorPage : MenuPage
    {
        #region Graphics
        TileSelection tileSelector;
        System.Drawing.Bitmap tileset;
        Texture2D[,] grid;
        ColorPicker colorPicker = new ColorPicker();
        EditorLayer[] layers = new EditorLayer[3];
        EditorLayer ActiveLayer;
        // Gère le menu tab
        bool oldState;
        private Rectangle tileSetBounds;
        public static Dictionary<System.Drawing.Color, List<int>> tiles_by_closest_color_list;

        #endregion

        #region Divers

        int cameraspeed;
        string path = Game1.IsRelease ? "" : "../../../../";

        #endregion

        #region Structure EditorTile

        public struct EditorTile
        {
            public System.Drawing.Color DominantColor;
            public int Index;
            public EditorTile(int index)
            {
                DominantColor = new System.Drawing.Color();
                Index = index;
            }
        }

        #endregion

        #region Buttons
        private SmallButton colorButton;
        #endregion

        #region Static
        public static EditorTile[] tile_list = new EditorTile[1024];
        public static int NombreTiles = 1024;
        public static int TailleTile = 32;    // En pixels, de chaque côté (une tile est un carré)
        public static int LargeurEnTiles = 32;
        public static int HauteurEnTiles = 32;
        public static int NombreCouleurs = 10;

        public static Camera camera = new Camera(Game1.graphics.GraphicsDevice.Viewport);
        public static MouseState ms;

        #endregion

        public MapEditorPage()
        {
            for (int i = 0; i < 3; i++)
            {
                layers[i] = new EditorLayer();
            }
            ActiveLayer = layers[0];
            #region Buttons

            for (int i = 0; i < 3; i++)
            {
                buttons.Add(new SmallButton("Layer " + (i + 1)));
                buttons[i].Position = new Vector2(95 * i, 10);
            }

            TileSelector = new TileSelection();
            colorButton = new SmallButton("Color");
            colorButton.Position = new Vector2(Game1.WindowWidth / 2 - colorButton.Width / 2, Game1.WindowHeight - colorButton.Height - 10);
            buttons.Add(colorButton);

            tileSetBounds = new Rectangle(0, 0, 32 * EditorLayer.numberOfTiles, 32 * EditorLayer.numberOfTiles);
            #endregion

            #region Graphics

            tileset = new System.Drawing.Bitmap(path + "Content/terrain.png");
            Texture2D gridtexture = Game1.Content.Load<Texture2D>(path + "Content/grid");
            cameraspeed = 10;

            // Initialisation de la grille de base
            grid = new Texture2D[HauteurEnTiles, LargeurEnTiles];
            for (int i = 0; i < HauteurEnTiles; i++)
            {
                for (int j = 0; j < LargeurEnTiles; j++)
                {
                    grid[j, i] = gridtexture;
                }
            }

            InitializeTiles();
            tiles_by_closest_color_list = new Dictionary<System.Drawing.Color, List<int>>();
            InitializeMostClosestColorsList();

            #endregion
        }

        #region Properties

        internal TileSelection TileSelector
        {
            get
            {
                return tileSelector;
            }

            set
            {
                tileSelector = value;
            }
        }

        #endregion

        public override MenuPage Action()
        {
            return null;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.End();

            var viewMatrix = camera.GetViewMatrix(this);

            spriteBatch.Begin(transformMatrix: viewMatrix);

            foreach (EditorLayer layer in layers)
            {
                layer.Draw(spriteBatch);
            }

            for (int i = 0; i < HauteurEnTiles; i++)
            {
                for (int j = 0; j < LargeurEnTiles; j++)
                {
                    spriteBatch.Draw(grid[j, i], new Vector2(j * 32, i * 32));
                }
            }

            spriteBatch.End();

            spriteBatch.Begin();

            TileSelector.Draw(spriteBatch);
            foreach (Button button in buttons)
            {
                button.Draw(spriteBatch);
            }

            if (ColorPicker.IsActive)
                colorPicker.Draw(spriteBatch);

            spriteBatch.End();

            spriteBatch.Begin();

        }

        public override void Update()
        {
            ms = Mouse.GetState();

            #region updating buttons
            for (int i = 0; i < 3; i++)
            {
                Button button = buttons[i];
                if (button.leftClick() && !button.selected)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        Button button2 = buttons[j];
                        button2.selected = false;
                    }
                    button.selected = true;
                    ActiveLayer = layers[i];
                }
            }

            #region affichage du colorpicker 
            bool tab = Keyboard.GetState().IsKeyDown(Keys.Tab);

            if (colorButton.leftClick() || tab && oldState != tab)
            {
                if (!colorButton.selected)
                    colorButton.selected = true;
                else
                    colorButton.selected = false;

                oldState = tab;

                ColorPicker.IsActive = colorButton.selected;
            }

            if (!tab)
                oldState = false;
            #endregion

            #endregion

            #region Update keyboard
            KeyboardState kb = Keyboard.GetState();

            if (kb.IsKeyDown(Keys.Left))
            {
                camera.Position = new Vector2(camera.Position.X - cameraspeed, camera.Position.Y);
            }
            if (kb.IsKeyDown(Keys.Right))
            {
                camera.Position = new Vector2(camera.Position.X + cameraspeed, camera.Position.Y);
            }
            if (kb.IsKeyDown(Keys.Up))
            {
                camera.Position = new Vector2(camera.Position.X, camera.Position.Y - cameraspeed);
            }
            if (kb.IsKeyDown(Keys.Down))
            {
                camera.Position = new Vector2(camera.Position.X, camera.Position.Y + cameraspeed);
            }
            if (kb.IsKeyDown(Keys.P)) // DEBUG
            {
                SaveStringArrayToFiles(path + "Content/maps/test");
            }
            if (kb.IsKeyDown(Keys.O)) // DEBUG
            {
                LoadStringMapIntoLevel(GetMapLayersFromFiles(path + "Content/maps/test/", "newmap", 3));
            }
            #endregion



            if (ms.LeftButton == ButtonState.Pressed && !ColorPicker.IsActive && !TileSelection.sourceRectangle.Contains(ms.Position))
            {
                Point MousePos = new Point(ms.Position.X + (int)camera.Position.X, ms.Position.Y + (int)camera.Position.Y);
                if (tileSetBounds.Contains(MousePos))
                {
                    int column = MousePos.X / EditorLayer.numberOfTiles;
                    int row = MousePos.Y / EditorLayer.numberOfTiles;

                    row = (MousePos.Y + (int)(row / 2)) / EditorLayer.numberOfTiles;

                    if (ms.LeftButton == ButtonState.Pressed)
                        Console.WriteLine("msx: " + MousePos.X + "\nmsy: " + MousePos.Y + "\ntilex: " + column + "\ntiley: " + row + "");
                    try
                    {
                        if (ActiveLayer.tiles[row, column].Index != TileSelection.ActiveTile.Index)
                            ActiveLayer.tiles[row, column] = TileSelection.ActiveTile;
                    }
                    catch (Exception)
                    {
                    }

                }
            }

        }

        #region Analyse de couleurs

        /// <summary>
        /// Initialise les tiles du tileset avec leur index et la couleur la plus présente
        /// (hormis la couleur vide (0,0,0,0))
        /// </summary>
        public void InitializeTiles()
        {
            // Variables 
            int currentRow = 0;
            int currentColumn = 0;

            // Boucle d'itération à travers tile_list
            for (int i = 0; i < 1024; i++)
            {
                currentColumn = i % LargeurEnTiles;
                if (i > 0 && currentColumn == 0)
                {
                    currentRow++;
                }

                tile_list[i] = new EditorTile(i);
                Dictionary<System.Drawing.Color, int> colors = new Dictionary<System.Drawing.Color, int>();

                for (int j = currentRow * TailleTile; j < (currentRow * TailleTile) + TailleTile; j++)
                {
                    for (int k = currentColumn * TailleTile; k < (currentColumn * TailleTile) + TailleTile; k++)
                    {

                        System.Drawing.Color pixel = tileset.GetPixel(k, j);
                        if (colors.ContainsKey(pixel))
                        {
                            colors[pixel]++;
                        }
                        else if (pixel.A != 0)
                        {
                            colors.Add(pixel, 1);
                        }
                    }
                }
                if (colors.Count == 0)
                    tile_list[i].DominantColor = System.Drawing.Color.FromArgb(0, 0, 0, 0);
                else
                    tile_list[i].DominantColor = colors.Aggregate((x, y) => x.Value > y.Value ? x : y).Key; // Récupère la couleur la plus trouvée
            }
        }

        /// <summary>
        /// Récupère la liste des couleurs les plus utilisées dans le tileset
        /// </summary>
        /// <param name="numberOfColors">Le nombre de couleurs à récupérer</param>
        /// <param name="tileset">L'image Bitmap du tileset</param>
        /// <returns>La liste comportant numberOfColors couleurs du tileset</returns>
        public List<System.Drawing.Color> GetMostUsedColorsInTileset(System.Drawing.Bitmap tileset)
        {
            if (NombreCouleurs < 1)
                throw new ArgumentOutOfRangeException("Le nombre de couleurs doit être strictement positif");
            Dictionary<System.Drawing.Color, int> colors = new Dictionary<System.Drawing.Color, int>();

            for (int i = 0; i < tileset.Height; i++)
            {
                for (int j = 0; j < tileset.Width; j++)
                {
                    System.Drawing.Color pixel = tileset.GetPixel(i, j);
                    if (colors.ContainsKey(pixel))
                        colors[pixel]++;
                    else if (pixel.A != 0)
                        colors.Add(pixel, 1);
                }
            }
            if (NombreCouleurs > colors.Count)
                NombreCouleurs = colors.Count;
            return new List<System.Drawing.Color>(colors.OrderByDescending(x => x.Value).Take(NombreCouleurs).ToDictionary(x => x.Key, x => x.Value).Keys); // Permet de récupérer un certain nombre de clef dans l'ordre décroissant du dictionnaire initial
        }

        /// <summary>
        /// Initialise le dictionnaire de couleurs les plus utilisées, rattachées avec une liste
        /// des couleurs correspondant le plus à ces couleurs
        /// </summary>
        public void InitializeMostClosestColorsList()
        {
            List<System.Drawing.Color> color_list = GetMostUsedColorsInTileset(tileset);
            foreach (System.Drawing.Color color in color_list)
            {
                tiles_by_closest_color_list.Add(color, new List<int>());
            }

            foreach (EditorTile tile in tile_list)
            {
                int diff = color_list.Select(n => ColorDiff(n, tile.DominantColor)).Min(n => n);

                // Trouvé sur internet : permet d'associer à une EditorTile une couleur proche selon une liste de couleurs
                System.Drawing.Color closestColor = color_list[color_list.FindIndex(n => ColorDiff(n, tile.DominantColor) == diff)];
                tiles_by_closest_color_list[closestColor].Add(tile.Index);
            }
        }

        #endregion

        #region Enregistrement des maps

        private string[] GetStringArrayFromLayers()
        {
            string[] res = new string[3];
            int mapIteration = 0;
            foreach (var layer in layers)
            {
                string map = "32;32;";
                int layerIteration = 0;
                foreach (var tile in layer.tiles)
                {
                    if (tile.Index == -1)
                        map += "507";
                    else
                        map += tile.Index;
                    layerIteration++;
                    if (layerIteration != NombreTiles)
                        map += ";";
                }
                res[mapIteration] = map;
                mapIteration++;
            }
            return res;
        }

        public void SaveStringArrayToFiles(string path)
        {
            var maps = GetStringArrayFromLayers();
            System.IO.Directory.CreateDirectory(path);
            int iteration = 0;
            foreach (var map in maps)
            {
                System.IO.File.WriteAllText(path + "/newmap" + ++iteration + ".todomap", map);
            }
        }

        private string[] GetMapLayersFromFiles(string path, string levelName, int layerNumber)
        {
            string[] res = new string[3];
            for (int i = 0; i < layerNumber; i++)
            {
                string map = System.IO.File.ReadAllText(path + levelName + (i + 1) + ".todomap");
                res[i] = map;
            }

            return res;
        }
        
        public void LoadStringMapIntoLevel(string[] maps)
        {
            int iteration = 0;
            foreach (var layer in layers)
            {
                string[] parsedMap = maps[iteration].Split(';');
                int mapLength = 0, mapWidth = 0;
                int row = 0;
                for (int i = 0; i < parsedMap.Length; i++)
                {
                    if (i == 0)
                        mapLength = int.Parse(parsedMap[i]);
                    else if (i == 1)
                        mapWidth = int.Parse(parsedMap[i]);
                    else
                    {
                        if (((i - 2) % mapLength == 0) && i != 2)
                            row++;
                        layer.tiles[row, (i - 2) % mapLength].Index = int.Parse(parsedMap[i]);
                    }

                }

                iteration++;
            }
        }

        #endregion

        #region Méthodes utiles

        /// <summary>
        /// Par TaW sur http://stackoverflow.com/questions/27374550/how-to-compare-color-object-and-get-closest-color-in-an-color
        /// </summary>
        /// <param name="c1">Couleur 1</param>
        /// <param name="c2">Couleur 2</param>
        /// <returns>La distance entre deux couleurs</returns>
        private int ColorDiff(System.Drawing.Color c1, System.Drawing.Color c2)
        {
            return (int)Math.Sqrt((c1.R - c2.R) * (c1.R - c2.R)
                                   + (c1.G - c2.G) * (c1.G - c2.G)
                                   + (c1.B - c2.B) * (c1.B - c2.B));
        }

        /// <summary>
        /// Convertit une couleur de type System.Drawing.Color en Microsoft.Xna.Framework.Color
        /// </summary>
        /// <param name="c">Couleur venant de System.Drawing.Color</param>
        /// <returns>Équivalent en Microsoft.Xna.Framework.Color</returns>
        private Color ConvertSystemDrawingColorToXNAColor(System.Drawing.Color c)
        {
            return new Color(c.R, c.G, c.B, c.A);
        }

        /// <summary>
        /// Convertit une couleur de type Microsoft.Xna.Framework.Color en System.Drawing.Color
        /// </summary>
        /// <param name="c">Couleur venant de Microsoft.Xna.Framework.Color</param>
        /// <returns>Équivalent en System.Drawing.Color</returns>
        private System.Drawing.Color ConvertXNAColorToSystemDrawingColor(Color c)
        {
            return System.Drawing.Color.FromArgb(c.A, c.R, c.G, c.B);
        }

        #endregion
    }
}