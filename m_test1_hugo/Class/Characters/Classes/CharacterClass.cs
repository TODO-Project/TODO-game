using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Main
{
    public abstract class CharacterClass
    {
        private int health;

        private int damageBonus;

        private int moveSpeed;

        #region
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

        public int DamageBonus
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
        #endregion
        //classe de personnage qu'on pourra choisir au début du jeu

    }
}