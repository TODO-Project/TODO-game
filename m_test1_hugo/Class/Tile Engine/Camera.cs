using m_test1_hugo.Class.Main.Menus.pages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Tile_Engine
{
    /// <summary>
    /// Décrit la caméra du jeu, qui suit le joueur sur la carte
    /// </summary>
    public class Camera
    {
        #region Fields

        /// <summary>
        /// Le niveau de zoom de la caméra (non utilisé)
        /// </summary>
        private float zoom;

        /// <summary>
        /// Le viewport (objet de transition vers l'écran propre au framework)
        /// </summary>
        private readonly Viewport viewport;

        /// <summary>
        /// La position de la caméra exprimée en vecteur 2D
        /// </summary>
        public static Vector2 position;

        /// <summary>
        /// L'origine de la caméra (non utilisé)
        /// </summary>
        private Vector2 origin;

        #endregion

        #region Properties

        /// <summary>
        /// Récupère et définit le niveau de zoom de la caméra entre 0 et 8.
        /// </summary>
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

        /// <summary>
        /// Récupère et définit la position de la caméra
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// Récupère et définit l'origine de la caméra (non utilisé)
        /// </summary>
        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Construit une caméra selon un viewport (celui de l'écran en général)
        /// </summary>
        /// <param name="viewport">Le viewport sur lequel la caméra se base</param>
        public Camera(Viewport viewport)
        {
            this.viewport = viewport;
            zoom = 1.0f;
            position = Vector2.Zero;
            origin = new Vector2(viewport.Width / 2f, viewport.Height / 2f);
        }

        /// <summary>
        /// Construit une caméra selon un viewport et une position initiale.
        /// </summary>
        /// <param name="viewport">Le viewport sur lequel la caméra se base</param>
        /// <param name="position">La position initiale de la caméra</param>

        public Camera(Viewport viewport, Vector2 position)
        {
            this.viewport = viewport;
            zoom = 1.0f;
            Camera.position = position;
            origin = new Vector2(GamePage.player.Center.X - viewport.Width / 2f, GamePage.player.Center.Y - viewport.Height / 2f);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Calcule et renvoie la matrice de transformation qui sera appliquée au spritebatch
        /// et qui fera en sorte de suivre le joueur
        /// </summary>
        /// <returns>Une matrice de transformation de la caméra</returns>
        public Matrix GetViewMatrix()
        {
            origin = new Vector2(GamePage.player.Center.X - viewport.Width / 2f, GamePage.player.Center.Y - viewport.Height / 2f);
            return
                Matrix.CreateTranslation(new Vector3(-Position, 0.0f)) *
                Matrix.CreateTranslation(new Vector3(-origin, 0.0f)) *
                Matrix.CreateScale(Zoom, Zoom, 1); 
        }

        #endregion
    }
}
