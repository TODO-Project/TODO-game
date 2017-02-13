using entrainementProjet1.Class.Main;
using m_test1_hugo.Class.ControlLayouts;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Main.outils_dev_jeu.Affects
{
    public static class CharacterAffect
    {
        public static void WeaponChange(Player player, Weapon weapon)
        {
            player.weapon = weapon;
        }
        
        public static void increaseMoveSpeed(Player player, int bonus)
        {
            player.MoveSpeed += bonus;
        }

        public static void decreaseMoveSpeed(Player player, int bonus)
        {
            player.MoveSpeed -= bonus;
        }

        public static void increaseDamages(Player player, int bonus)
        {
            player.damageBonus += bonus;
        }
        public static void decreaseDamages(Player player, int bonus)
        {
            player.damageBonus -= bonus;
        }

        public static void increaseHealth(Player player, int bonus)
        {
            player.Health += bonus;
            player.MaxHealth += bonus;
        }
        public static void decreaseHealth(Player player, int bonus)
        {
            player.Health -= bonus;
            player.MaxHealth -= bonus;
        }


    }
}
