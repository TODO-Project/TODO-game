using m_test1_hugo.Class.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using m_test1_hugo.Class.Tile_Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static m_test1_hugo.Class.gamestates.pages.Editor.MapEditorPage;

namespace m_test1_hugo.Class.gamestates.pages.Editor
{
    class TileSelection : Sprite
    {
        #region attributes
        public static Rectangle sourceRectangle;
        Texture2D bgTex;
        public static EditorTile ActiveTile;
        private bool isActive = false;
        private int previousScroll;
        private int maxView, minView;// pour le scroll
        #endregion

        #region construct
        public TileSelection()
        {
            LoadContent(Game1.Content);
            ActiveTile = new EditorTile(0);
            maxView = 40;
            minView = 0;
            previousScroll = Mouse.GetState().ScrollWheelValue;
        }
        #endregion

        #region properties

        #endregion

        #region methods
        public void Initialize()
        {

        }

        public void ScrollDown()
        {
            maxView += 6;
            minView += 6;
        }

        public void ScrollUp()
        {
            maxView -= 6;
            minView -= 6;
        }

        public void Update()
        {
            MouseState mouse = Mouse.GetState();

            isActive = Bounds.Contains(mouse.Position);

            if (mouse.ScrollWheelValue > previousScroll)
            {
                ScrollUp();
                int previousScroll = mouse.ScrollWheelValue;
            }

            if (mouse.ScrollWheelValue < previousScroll)
            {
                ScrollDown();
                int previousScroll = mouse.ScrollWheelValue;
            }


        }

        public override void LoadContent(ContentManager content)
        {
            this.bgTex = content.Load<Texture2D>("players/green");
            sourceRectangle = new Rectangle(0, 0, Game1.WindowWidth / 6 - 30, Game1.WindowHeight);
            texture = content.Load<Texture2D>("terrain");
        }

        public new void Draw(SpriteBatch sp)
        {
            Update();
            sp.Draw(bgTex, sourceRectangle, Color.DarkGray);

            int x = 10;
            int y = 80;

            if (ColorPicker.ActiveColor != System.Drawing.Color.Empty)
            {
                int min = 0;
                int max = 0;
                foreach (int tileIndex in tiles_by_closest_color_list[ColorPicker.ActiveColor])
                {
                    if (min >= minView && max <= maxView)
                    {
                        int column = (int)(tileIndex / EditorLayer.numberOfTiles);
                        int row = tileIndex % EditorLayer.numberOfTiles;

                        Rectangle rect = new Rectangle(row * 32, column * 32, 32, 32);
                        Rectangle destinationRectangle = new Rectangle(x, y, 32, 32);

                        sp.Draw(texture, destinationRectangle, rect, Color.White);

                        x += 40;

                        if (x > sourceRectangle.Width + sourceRectangle.X - 40)
                        {
                            x = 10;
                            y += 40;
                        }

                        if (destinationRectangle.Contains(MapEditorPage.ms.Position) && MapEditorPage.ms.LeftButton == ButtonState.Pressed)
                        {
                            if (ActiveTile.Index != tileIndex)
                            {
                                ActiveTile = new EditorTile(tileIndex);
                            }
                        }
                    }
                    min++;
                    max++;
                }
            }
        }
        #endregion
    }
}
