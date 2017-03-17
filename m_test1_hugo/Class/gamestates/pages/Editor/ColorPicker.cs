using m_test1_hugo.Class.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace m_test1_hugo.Class.gamestates.pages.Editor
{
    class ColorPicker : Sprite
    {
        #region attributs
        public static bool IsActive;
        Texture2D whiteTex;
        public static System.Drawing.Color ActiveColor;
        private int rectSize = 150;
        #endregion

        #region prop
        #endregion

        public ColorPicker()
        {
            LoadContent(Game1.Content);
        }
        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("editor/colorPicker");
            Position = this.WindowCenter;
            whiteTex = content.Load<Texture2D>("players/green");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);

            int x = (int)this.Position.X + 10;
            int y = (int)this.Position.Y + 10;

            foreach (System.Drawing.Color currentColor in MapEditorPage.tiles_by_closest_color_list.Keys)
            {
                Rectangle rect = new Rectangle(x, y, rectSize, rectSize);
                Color color = new Color(currentColor.R, currentColor.G, currentColor.B);
                spriteBatch.Draw(whiteTex, rect, color);
                if (rect.Contains(MapEditorPage.ms.Position) && MapEditorPage.ms.LeftButton == ButtonState.Pressed)
                    ActiveColor = currentColor;
                x += rectSize + 10;

                if (x > Position.X + Width - rectSize + 10)
                {
                    x = (int)Position.X + 10;
                    y += rectSize + 10;
                }
            }
        }
    }
}
