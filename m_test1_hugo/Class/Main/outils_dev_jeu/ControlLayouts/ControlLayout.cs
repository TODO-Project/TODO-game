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
    public abstract class ControlLayout
    {
        /*public abstract Dictionary<string, bool> Controls
        {
            get; set;
        }*/

        //++ definir clavier / souris

        public abstract bool MoveDown
        {
            get;
        }
        public abstract bool MoveUp
        {
            get;
        }

        public abstract bool MoveLeft
        {
            get;
        }

        public abstract bool MoveRight
        {
            get;
        }

        public abstract bool Reload
        {
            get;
        }

        public abstract bool Use
        {
            get;
        }

        public abstract bool Shoot
        {
            get;
        }

/*        public abstract Vector2 CursorPosition
        {
            get;
        }*/

        public abstract float CursorPosX
        {
            get;
        }

        public abstract float CursorPosY
        {
            get;
        }

        public float MenuCursorPosX
        {
            get { return Game1.WindowWidth / 2 + Mouse.GetState().Position.X; }
        }

        public float MenuCursorPosY
        {
            get { return Game1.WindowHeight / 2 + Mouse.GetState().Position.Y; }
        }
    }
}
