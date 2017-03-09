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

namespace m_test1_hugo.Class.gamestates.pages.Editor
{
    class TileSelection : Sprite
    {
        #region attributes
        Rectangle sourceRectangle;
        Texture2D bgTex;
        private List<Tile> tileList;
        private bool isActive = false;
        #endregion

        #region construct
        public TileSelection()
        {
            LoadContent(Game1.Content);
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

        }

        public void ScrollUp()
        {

        }

        public void Update()
        {
            MouseState mouse = Mouse.GetState();
            if (Bounds.Contains(mouse.Position))
                isActive = true;
            else
                isActive = false;

            if(isActive)
            {
                int previousScroll = mouse.ScrollWheelValue;

                if(mouse.ScrollWheelValue > previousScroll )
                    ScrollUp();
                if(mouse.ScrollWheelValue < previousScroll)
                    ScrollDown();
            }
        }

        public override void LoadContent(ContentManager content)
        {
            this.bgTex = content.Load<Texture2D>("players/green");
            this.sourceRectangle = new Rectangle(0, 0, Game1.WindowWidth / 6, Game1.WindowHeight);
        }

        public new void Draw(SpriteBatch sp)
        {
            sp.Draw(bgTex, sourceRectangle, Color.DarkSlateGray);
        }
        #endregion
    }
}
