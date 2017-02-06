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
        public abstract int MoveSpeed
        {
            get;
            set;
        }
        public abstract int Health
        {
            get;
            set;
        }

        public abstract int DamageBonus
        {
            get;
            set;
        }
    }
}
