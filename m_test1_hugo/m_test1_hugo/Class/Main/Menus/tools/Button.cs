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
        string buttonName { get; set; }

        public Button(string ButtonName)
        {
            this.buttonName = ButtonName;
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("menus/button"+buttonName);
        }

        public bool leftClick()
        {
            if(Bounds.Contains(Menu.curMousePos.X, Menu.curMousePos.Y) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }

        public bool rightClick()
        {
            if (Bounds.Contains(Menu.curMousePos.X, Menu.curMousePos.Y) && Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }

    }
}
