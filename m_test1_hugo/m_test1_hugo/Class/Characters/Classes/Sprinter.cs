using System;
using m_test1_hugo.Class.Main;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace m_test1_hugo.Class.Characters
{
    class Sprinter : CharacterClass
    {
        #region attributs
        private int health;
        public override int Health
        {
            get { return this.health ;}
            set { health = value; }
        }

        private int moveSpeed;
        public override int MoveSpeed
        {
            get {return moveSpeed; }
            set { this.moveSpeed = value; }
        }

        private int damageBonus;
        public override int DamageBonus
        {
            get
            {
                return damageBonus;
            }

            set
            {
                damageBonus = value;
            }
        }

        
        #endregion
        public Sprinter()
        {
            MoveSpeed = 5;
            this.DamageBonus= 1;
            this.Health = 80;
        }
    }
}
