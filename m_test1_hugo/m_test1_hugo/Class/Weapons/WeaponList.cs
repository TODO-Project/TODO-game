using m_test1_hugo.Class.Main;
using m_test1_hugo.Class.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Content.weapons
{
    class WeaponList
    {
        public List<Weapon> List = new List<Weapon>();

        WeaponList()
        {
            List.Add( new Sniper() );
            List.Add( new Assault() );
        }
    }
}
