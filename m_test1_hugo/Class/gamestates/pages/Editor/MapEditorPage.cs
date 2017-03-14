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
        TileSelection tileSelector;
        EditorTile[] tile_list = new EditorTile[1024];
        Camera camera;
        Texture2D[,] grid;
        int cameraspeed;
        string path = Game1.IsRelease ? "" : "../../../../";

        #region tile
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


        #region buttons
        private SmallButton colorButton;
        #endregion

        public MapEditorPage()
        {
            #region Buttons
            for (int i = 0; i < 3; i++)
            {
                buttons.Add(new SmallButton("Layer " + (i + 1)));
                buttons[i].Position = new Vector2(15 + 95 * i, 120);
            }

            TileSelector = new TileSelection();
            colorButton = new SmallButton("Color");
            colorButton.Position = new Vector2(Game1.WindowWidth / 2 - colorButton.Width / 2, Game1.WindowHeight - colorButton.Height - 10);
            buttons.Add(colorButton);
            #endregion

            #region graphics
            camera = new Camera(Game1.graphics.GraphicsDevice.Viewport);
            Texture2D gridtexture = Game1.Content.Load<Texture2D>(path + "Content/grid");
            cameraspeed = 5;
            grid = new Texture2D[32, 32];
            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < 32; j++)
                {
                    grid[j, i] = gridtexture;
                }
            }
            InitializeTiles();
            #endregion
        }

        #region prop
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

            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < 32; j++)
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



            spriteBatch.End();

            spriteBatch.Begin();

        }



        public override void Update()
        {
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
                }
            }

            if (colorButton.leftClick())
            {
                if (!colorButton.selected)
                    colorButton.selected = true;
                else
                    colorButton.selected = false;
            }

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
                Console.WriteLine(camera.Position);
            }
            #endregion

            #region Update mouse
            MouseState ms = Mouse.GetState();
            Vector2 realPosition = Vector2.Add(ms.Position.ToVector2(), camera.Position);

            if (ms.LeftButton == ButtonState.Pressed)
            {
                Console.WriteLine(realPosition);
            }

            #endregion
        }

        public void InitializeTiles()
        {
            int numberOfTiles = 32;
            int currentRow = 0;
            int currentColumn = 0;
            System.Drawing.Bitmap tileset = new System.Drawing.Bitmap(path + "Content/terrain.png");
            for (int i = 0; i < 1024; i++)
            {
                currentColumn = i % numberOfTiles;
                if (i > 0 && currentColumn == 0)
                {
                    currentRow++;
                }

                tile_list[i] = new EditorTile(i);
                Dictionary<System.Drawing.Color, int> colors = new Dictionary<System.Drawing.Color, int>();
                //Console.WriteLine("Pour tile[" + currentColumn + ", " + currentRow + "] : X(" + currentRow * 32 + " - " + ((currentRow * 32) + 32) + ") Y(" + currentColumn * 32 + " - " + ((currentColumn * 32) + 32) + ")");
                for (int j = currentRow * 32; j < (currentRow * 32) + 32; j++)
                {
                    for (int k = currentColumn * 32; k < (currentColumn * 32) + 32; k++)
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

    }
}
