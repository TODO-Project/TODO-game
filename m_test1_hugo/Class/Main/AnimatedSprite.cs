using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace m_test1_hugo.Class.Main
{
    /// <summary>
    /// sprite anime
    /// </summary>
    public abstract class AnimatedSprite : Sprite
    {
        #region encapsulation des attributs

        public int columns;
        public int rows;
        public int currentRow;
        public int currentColumn;
        public bool isMoving;
        public bool isMovingUp;

        public new int Height
        {
            get { return texture.Height / rows; }
        }

        public new int Width
        {
            get { return texture.Width / columns; }
        }

        public new Vector2 Center
        {
            get { return new Vector2(Position.X + Width / 2, Position.Y + Height / 2+ 12); }
        }

        private int currentFrame = 0;

        private int totalFrames { get { return rows * columns; } }

        
        private DateTime timeSinceLastFrame;
        // slow down frame animation 
        private int millisecondsPerFrame = 120;

        private Rectangle _sourceRectangle;
        public Rectangle sourceRectangle
        {
            get { return new Rectangle(Width * currentColumn, Height * currentRow, Width, Height); }
            set { _sourceRectangle = value; }
        }

        private Rectangle _destinationRectangle;
        public Rectangle destinationRectangle
        {
            get { return _destinationRectangle; }
            set { _destinationRectangle = value; }
        }
        #endregion


        public void UpdateSprite(GameTime gameTime)
        {
            destinationRectangle = new Rectangle(
                 (int)Position.X,
                 (int)Position.Y,
                 (int)Width,
                 (int)Height
             );

            if (isMoving)
            {
                
                if (DateTime.Now >= timeSinceLastFrame.AddMilliseconds(millisecondsPerFrame)) //si le delai est bien depasse
                {
                    timeSinceLastFrame = DateTime.Now; 
                    currentColumn++;// on change de frame
                    if (currentColumn == columns)
                    {
                        currentColumn = 0;
                    }
                }
            }
        }

        public void LoadContent(ContentManager content, string textureName, int Rows, int Columns)
        {
            this.rows = Rows;
            this.columns = Columns;
            texture = Game1.Content.Load<Texture2D>(textureName);
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
