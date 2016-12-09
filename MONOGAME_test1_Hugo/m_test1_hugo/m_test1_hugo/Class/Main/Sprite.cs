using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Runtime.Remoting.Contexts;

namespace m_test1_hugo.Class.Main
{
    public abstract class Sprite : Drawable
    {
        protected Texture2D texture;
        public Vector2 Position;
        public int Height
        {
            get { return texture.Height; }
        }

        public int Width
        {
            get { return texture.Width; }
        }

        public Vector2 Center
        {
            get { return new Vector2(Width/2, Height / 2); }
        }

        public Sprite()
        {
            Position = Vector2.Zero;
        }  

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }

        public abstract void LoadContent(ContentManager content);
    }
}
