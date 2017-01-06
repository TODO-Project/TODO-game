using entrainementProjet1.Class.Main;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Main
{
    public abstract class Character : AnimatedSprite
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

        private double health;
        public double Health
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

        private int armor;
        public int Armor { get; set; } 

        #endregion

        public void moveLeft()
        {
            isMoving = true;
            if (this.Position.X >= 0 + this.MoveSpeed)
                this.Position = new Vector2(this.Position.X - this.MoveSpeed, this.Position.Y);
        }

        public void moveRight()
        {
            isMoving = true;
            if (this.Position.X + this.Width <= Game1.WindowWidth - this.MoveSpeed)
                this.Position = new Vector2(this.Position.X + this.MoveSpeed, this.Position.Y);
        }

        public void moveDown()
        {
            isMoving = true;
            if (this.Position.Y + this.Height <= Game1.WindowHeight - this.MoveSpeed)
                this.Position = new Vector2(this.Position.X, this.Position.Y + this.MoveSpeed);
        }

        public void moveUp()
        {
            isMoving = true;
            if (this.Position.Y >= 0 + this.MoveSpeed)
                this.Position = new Vector2(this.Position.X, this.Position.Y - this.MoveSpeed);
        }

        //public List<Cloth> Clothing;

    }
}
