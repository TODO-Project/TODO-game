using m_test1_hugo.Class.Main.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace m_test1_hugo.Class.Main.Menus
{
    class Button : Sprite, Clickable
    {
        MouseState ms = Mouse.GetState();

        public override void LoadContent(ContentManager content)
        {
            this.texture = content.Load<Texture2D>("button");
        }

        public Vector2 curMousePos
        {
            get { return new Vector2(ms.X, ms.Y);}
        }
        
        public bool leftClick
        {
           get { return texture.Bounds.Contains(curMousePos.X, curMousePos.Y) && ms.LeftButton == ButtonState.Pressed; }  
        }

        public bool rightClick
        {
            get { return texture.Bounds.Contains(curMousePos.X, curMousePos.Y) && ms.RightButton == ButtonState.Pressed; }
        }

        public Button(Vector2 position)
        {
            this.Position = position;
        }

    }
}
