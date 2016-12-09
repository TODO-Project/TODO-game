using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Main
{
    interface Drawable
    {
        void LoadContent(ContentManager content);
        void Draw(SpriteBatch spriteBatch);
    }
}
