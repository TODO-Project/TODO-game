using m_test1_hugo.Class.ControlLayouts;
using m_test1_hugo.Class.Main.Menus.pages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Main.outils_dev_jeu.ControlLayouts
{
    class GamePadController:ControlLayout
    {
        GamePadState gp
        {
            get { return GamePage.gp; }
        }

        public override bool MoveDown
        {
            get { return gp.IsButtonDown(Buttons.LeftThumbstickDown); }
        }

        public override bool MoveUp
        {
            get { return gp.IsButtonDown(Buttons.LeftThumbstickUp); }
        }

        public override bool MoveLeft
        {
            get { return gp.IsButtonDown(Buttons.LeftThumbstickLeft); }
        }

        public override bool MoveRight
        {
            get { return gp.IsButtonDown(Buttons.LeftThumbstickRight); }
        }

        public override bool Reload
        {
            get { return gp.IsButtonDown(Buttons.X); }
        }

        public override bool Use
        {
            get { return gp.IsButtonDown(Buttons.A); }
        }

        public override bool Shoot
        {
            get { return gp.IsButtonDown(Buttons.RightShoulder); }
        }

        /**public override Vector2 CursorPosition
        {
            get { return Game1.gp.ThumbSticks.Right; }
        }*/

        public override float CursorPosX
        {
            get { return GamePage.gp.ThumbSticks.Right.X; }
        }

        public override float CursorPosY
        {
            get { return GamePage.gp.ThumbSticks.Right.Y; }
        }
    }
}
