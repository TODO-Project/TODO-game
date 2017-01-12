using m_test1_hugo.Class.Main.interfaces;
using m_test1_hugo.Class.Tile_Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Main
{
    public abstract class Character : AnimatedSprite, TileCollision, SpriteCollision
    {

        #region attributs 
        private string pseudo;
        public string Pseudo
        {
            get
            {
                return pseudo;
            }

            set
            {
                pseudo = value;
            }
        }

        public bool IsDead()
        {
            return this.Health <= 0;
        }

        public Weapon weapon;

        private int moveSpeed;
        public int MoveSpeed
        {
            get
            {
                return moveSpeed;
            }

            set
            {
                moveSpeed = value;
            }
        }

        private int health;
        public int Health
        {
            get
            {
                return health;
            }

            set
            {
                health = value;
            }
        }

        private int _maxHealth;
        public int MaxHealth
        {
            get { return _maxHealth; }
            set { _maxHealth = value; }
        }

        private int armor;
        public int Armor { get; set; } 

        #endregion

        public void moveLeft(int tileSize, int mapWidth, int mapHeight, CollisionLayer collisionLayer, Camera camera, GameTime gametime)
        {
            // var deltaTime = (float)gametime.ElapsedGameTime.TotalSeconds;
            isMoving = true;
            if (TileCollision(this, tileSize, mapWidth, mapHeight, collisionLayer, 0))
            {
                this.Position = new Vector2(this.Position.X - this.MoveSpeed, this.Position.Y);
                camera.Position -= new Vector2(MoveSpeed, 0);
            }
        }

        public void moveRight(int tileSize, int mapWidth, int mapHeight, CollisionLayer collisionLayer, Camera camera, GameTime gametime)
        {
            // var deltaTime = (float)gametime.ElapsedGameTime.TotalSeconds;
            isMoving = true;
            if (TileCollision(this, tileSize, mapWidth, mapHeight, collisionLayer, 1))
            {
                this.Position = new Vector2(this.Position.X + this.MoveSpeed, this.Position.Y);
                camera.Position += new Vector2(MoveSpeed, 0);
            }
        }

        public void moveDown(int tileSize, int mapWidth, int mapHeight, CollisionLayer collisionLayer, Camera camera, GameTime gametime)
        {
            // var deltaTime = (float)gametime.ElapsedGameTime.TotalSeconds;
            isMoving = true;
            if (TileCollision(this, tileSize, mapWidth, mapHeight, collisionLayer, 3))
            {
                this.Position = new Vector2(this.Position.X, this.Position.Y + this.MoveSpeed);
                camera.Position += new Vector2(0, MoveSpeed);
            }
        }

        public void moveUp(int tileSize, int mapWidth, int mapHeight, CollisionLayer collisionLayer, Camera camera, GameTime gametime)
        {
            // var deltaTime = (float)gametime.ElapsedGameTime.TotalSeconds;
            isMoving = true;
            if (TileCollision(this, tileSize, mapWidth, mapHeight, collisionLayer, 2))
            {
                this.Position = new Vector2(this.Position.X, this.Position.Y - this.MoveSpeed);
                camera.Position -= new Vector2(0, MoveSpeed);
            }
        }

        public bool TileCollision(Sprite objet1, int tileSize, int mapWidth, int mapHeight, CollisionLayer collisionLayer, int direction)
        {
            int tileX = (int)Math.Ceiling(((this.Center.X) / mapWidth) - 1) ;
            int tileY = (int)Math.Ceiling(((this.Center.Y) / mapHeight) - 1);

            switch (direction)
            {
                case 0:     // Gauche
                    if (tileX > 0)
                        tileX--;
                    break;
                case 1:     // Droite
                    if (tileX < mapWidth)
                        tileX++;
                    break;
                case 2:     // Haut
                    if (tileY > 0)
                        tileY--;
                    break;
                case 3:     // Bas
                    if (tileY < mapHeight)
                        tileY++;
                    break;
                default:
                    break;
            }
            return collisionLayer.GetTile(tileX, tileY);
        }

        public bool SpriteCollision(Rectangle objet1)
        {
            return false;
        }

        //public List<Cloth> Clothing;

    }
}
