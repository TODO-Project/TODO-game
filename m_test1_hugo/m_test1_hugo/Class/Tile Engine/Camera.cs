using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Tile_Engine
{
    public class Camera
    {
        #region Fields

        private float zoom;
        private readonly Viewport viewport;
        public static Vector2 position;
        private Vector2 origin;

        #endregion

        #region Properties

        public float Zoom
        {
            get { return zoom; }
            set
            {
                zoom = value;
                if (zoom > 8.0f)
                    zoom = 8.0f;
            }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        #endregion

        #region Constructors

        public Camera(Viewport viewport)
        {
            this.viewport = viewport;
            zoom = 1.0f;
            position = Vector2.Zero;
            origin = new Vector2(viewport.Width / 2f, viewport.Height / 2f);
        }

        public Camera(Viewport viewport, Vector2 position)
        {
            this.viewport = viewport;
            zoom = 1.0f;
            Camera.position = position;
            origin = new Vector2(viewport.Width / 2f, viewport.Height / 2f);
        }

        #endregion

        #region Methods

        public Matrix GetViewMatrix()
        {
            return
                Matrix.CreateTranslation(new Vector3(-Position, 0.0f)) *
                Matrix.CreateTranslation(new Vector3(-origin, 0.0f)) *
                Matrix.CreateScale(Zoom, Zoom, 1); 
        }

        #endregion
    }
}
