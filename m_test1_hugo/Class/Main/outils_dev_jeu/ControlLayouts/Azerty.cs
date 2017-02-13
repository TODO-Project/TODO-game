using m_test1_hugo.Class.Main.Menus.pages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.ControlLayouts
{
    class Azerty : ControlLayout
    {
        /*  public Dictionary<string, bool> _controls = new Dictionary<string, bool>();
          public override Dictionary<string, bool> Controls
          {
              get { return _controls; }
              set { _controls = new Dictionary<string, bool>(); }
          }

          public bool W
          {
              get { return Keyboard.GetState().IsKeyDown(Keys.W); }
          }

          public Azerty()
          {
              Controls.Add("forward", W );
              /*Controls.Add("forward", W);
              /*Controls.Add("left", Keys.C);
              Controls.Add("right", Keys.D);
              Controls.Add("use", Keys.E);
          }*/
        
        public override bool MoveDown
        {
            get { return Keyboard.GetState().IsKeyDown(Keys.S); }
        }

        public override bool MoveUp
        {
            get { return Keyboard.GetState().IsKeyDown(Keys.Z); }
        }

        public override bool MoveLeft
        {
            get { return Keyboard.GetState().IsKeyDown(Keys.Q); }
        }

        public override bool MoveRight
        {
            get { return Keyboard.GetState().IsKeyDown(Keys.D); }
        }

        public override bool Reload
        {
            get { return Keyboard.GetState().IsKeyDown(Keys.R); }
        }

        public override bool Use
        {
            get { return Keyboard.GetState().IsKeyDown(Keys.E); }
        }

        public override bool Shoot
        {
            get { return Mouse.GetState().LeftButton == ButtonState.Pressed; }
        }

        /*public override Vector2 CursorPosition
        {
            get { return Game1.ms.Position.ToVector2(); }
        }*/

        public override float CursorPosX
        {
            get { return GamePage.camera.Origin.X + GamePage.ms.Position.X; }
        }

        public override float CursorPosY
        {
            get { return GamePage.camera.Origin.Y + GamePage.ms.Position.Y; }
        }
    }
}
