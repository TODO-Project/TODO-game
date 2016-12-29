using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Main
{
    abstract class Character : Sprite
    {
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

        private string characterClass;
        public string CharacterClass
        {
            get
            {
                return characterClass;
            }

            set
            {
                characterClass = value;
            }
        }

        public bool IsDead()
        {
            return this.Health <= 0;
        }

        private double moveSpeed;
        public double MoveSpeed
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

        private int magicalProtection;
        public int MagicalProtection
        {
            get
            {
                return magicalProtection;
            }

            set
            {
                magicalProtection = value;
            }
        }

        private int physicalProtection;
        public int PhysicalProtection
        {
            get
            {
                return PhysicalProtection;
            }

            set
            {
                PhysicalProtection = value;
            }
        }

        private double damageMultiplier;
        public double DamageMultiplier
        {
            get
            {
                return damageMultiplier;
            }

            set
            {
                damageMultiplier = value;
            }
        }
        
        //private hitbox hitbox;




        //private Sprite sprite;

        //private List<Clothes> Clothing;


    }
}
