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
        private int numberOfTilesFound;
        private Rectangle scrollBar;
        private int scrollBarX;
        public static int maxView, minView;// pour le scroll
        #endregion

        #region construct
        public TileSelection()
        {
            LoadContent(Game1.Content);
            ActiveTile = new EditorTile(507);
            maxView = 60;
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
            if (maxView + 6 <= numberOfTilesFound)
            {
                maxView += 6;
                minView += 6;
                scrollBarX += 10;
            }
            else if(maxView <= numberOfTilesFound)
            {
                maxView += 6;
                minView += 6;
            }

        }

        public void ScrollUp()
        {
            if (minView - 6 >= 0)
            {
                minView -= 6;
                maxView -= 6;
                scrollBarX -= 10;
            }
            else
                minView = 0;
        }

        public void Update()
        {
            MouseState mouse = Mouse.GetState();

            isActive = Bounds.Contains(mouse.Position);

            if(mouse.ScrollWheelValue > previousScroll )
            {
                ScrollUp();
                previousScroll = mouse.ScrollWheelValue;
            }
                
            if(mouse.ScrollWheelValue < previousScroll)
            {
                ScrollDown();
                previousScroll = mouse.ScrollWheelValue;
            }
                

        }

        public override void LoadContent(ContentManager content)
        {
            this.bgTex = content.Load<Texture2D>("players/green");
            sourceRectangle = new Rectangle(0, 0, 285, Game1.WindowHeight);
            texture = content.Load<Texture2D>("terrain");
        }

        public new void Draw(SpriteBatch sp)
        {
            Update();
            sp.Draw(bgTex, sourceRectangle, new Color(Color.Gray, 0.2f) );

            int x = 10;
            int y = 80;

            if (ColorPicker.ActiveColor != System.Drawing.Color.Empty)
            {
                int min = 0;
                int max = 0;
                numberOfTilesFound = tiles_by_closest_color_list[ColorPicker.ActiveColor].Count;
                //scrollBar = new Rectangle(200, scrollBarX, 20, (numberOfTilesFound*32*100/7)/(16*(32)) );
                sp.Draw(bgTex, scrollBar, Color.Red);
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

                        if (x > sourceRectangle.Width + sourceRectangle.X - 40 - 40)
                        {
                            x = 10;
                            y += 40;
                        }

                        if (destinationRectangle.Contains(MapEditorPage.ms.Position) && MapEditorPage.ms.LeftButton == ButtonState.Pressed)
                        {
                            if (ActiveTile.Index != tileIndex)
                                ActiveTile = new EditorTile(tileIndex);
                        }
                    }
                    min++;
                    max++;
                }
            }
            else
                DisplayAllTiles(sp, x, y);
            
        }

        public void DisplayAllTiles(SpriteBatch sp, int x, int y)
        {
            /*int min = 0;
            int max = 0;
            numberOfTilesFound = tiles_by_closest_color_list.Count;
            //scrollBar = new Rectangle(200, scrollBarX, 20, (numberOfTilesFound*32*100/7)/(16*(32)) );
            sp.Draw(bgTex, scrollBar, Color.Red);
            foreach (int tileIndex in tiles_by_closest_color_list)
            {
                if (min >= minView && max <= maxView)
                {
                    int column = (int)(tileIndex / EditorLayer.numberOfTiles);
                    int row = tileIndex % EditorLayer.numberOfTiles;

                    Rectangle rect = new Rectangle(row * 32, column * 32, 32, 32);
                    Rectangle destinationRectangle = new Rectangle(x, y, 32, 32);

                    sp.Draw(texture, destinationRectangle, rect, Color.White);

                    x += 40;

                    if (x > sourceRectangle.Width + sourceRectangle.X - 40 - 40)
                    {
                        x = 10;
                        y += 40;
                    }

                    if (destinationRectangle.Contains(MapEditorPage.ms.Position) && MapEditorPage.ms.LeftButton == ButtonState.Pressed)
                    {
                        if (ActiveTile.Index != tileIndex)
                            ActiveTile = new EditorTile(tileIndex);
                    }
                }
                min++;
                max++;
            }
            */
        }
        #endregion
    }
}
