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

        
        private int timeSinceLastFrame = 0;
        // slow down frame animation 
        private int millisecondsPerFrame = 120;

        private Rectangle _sourceRectangle;
        public Rectangle sourceRectangle
        {
            get { return _sourceRectangle; }
            set { _sourceRectangle = value; }
        }

        #endregion

        public void Update(GameTime gameTime)
        {
            if (isMoving)
            {
                timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                if (timeSinceLastFrame >= millisecondsPerFrame)
                {
                    //timeSinceLastFrame -= millisecondsPerFrame;

                    // increment current frame
                    // currentFrame++;
                    currentColumn++;
                    timeSinceLastFrame = 0;
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
            texture = content.Load<Texture2D>(textureName);
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle(Width * currentColumn , Height*currentRow, Width, Height);
            Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);

            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
