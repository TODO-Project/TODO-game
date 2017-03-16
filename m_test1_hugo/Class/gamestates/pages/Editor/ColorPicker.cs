using m_test1_hugo.Class.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace m_test1_hugo.Class.gamestates.pages.Editor
{
    class ColorPicker : Sprite
    {
        #region attributs
        public bool IsActive;
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
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);

        }
    }
}
