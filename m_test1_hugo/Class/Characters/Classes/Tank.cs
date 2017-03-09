using m_test1_hugo.Class.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Characters.Classes
{
    class Tank : CharacterClass
    {
        public Tank()
        {
            this.DamageBonus = 0;
            this.Health = 125;
            this.MoveSpeed = 4;
        }
    }
}
