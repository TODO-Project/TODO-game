using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Runtime.Remoting.Contexts;
using m_test1_hugo.Class.Main.interfaces;

namespace m_test1_hugo.Class.Main
{
    public abstract class Sprite : Drawable
    {
        public Texture2D texture;

        // idem rq je ne sais plus ou, stocke ta dernière position
        private Vector2 _position;

        //Mettre virtual pour pouvoir l'ovverider dans sniper
        // Et idem rq je ne sais plus pour le getter et setter
        virtual public Vector2 Position 
        {
            get
            {
                return this._position;
            } 
            
            set
            {
                this._position = value;
            }
        }

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
            get { return new Vector2(Position.X + (Width / 2), Position.Y + (Height / 2)); }
        }

        private Rectangle bounds;
        public Rectangle Bounds
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Width, Height); }
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
