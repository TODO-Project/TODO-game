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

        #region Fields

        private bool LeftClick;
        private bool RightClick;

        #endregion

        #region Properties

        string buttonName { get; set; }

        public bool leftClick
        {
            get { return Bounds.Contains(Menu.curMousePos.X, Menu.curMousePos.Y) && Menu.ms.LeftButton == ButtonState.Pressed; }
            set { LeftClick = value; }
        }


        public bool rightClick
        {
            get { return Bounds.Contains(Menu.curMousePos.X, Menu.curMousePos.Y) && Menu.ms.RightButton == ButtonState.Pressed; }
            set { LeftClick = value; }
        }

        #endregion

        #region Constructors

        public Button(string ButtonName)
        {
            this.buttonName = ButtonName;
        }

        #endregion

        #region Methods

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("menus/button"+buttonName);
        }

        #endregion

    }
}
