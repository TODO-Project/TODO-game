using entrainementProjet1.Class.Main;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Main.outils_dev_jeu.Affects
{
    class CharacterAffect
    {
        public static void WeaponChange(Player player, Weapon weapon)
        {
            Vector2 CurrentPlayerPos = Game1.player.Position;
            int CurrentPlayerRow = Game1.player.currentRow;
            int CurrentPlayerColumn = Game1.player.currentColumn;
            float CurrentMouseRotation = Game1.player.MouseRotationAngle;
            int currentHealth = Game1.player.Health;
            Cloth[] currentClothList = player.ClothesList;
            Player.CharacterList.Remove(player);

            Game1.player = new Player(Game1.player.classe, weapon, player.team);
            Game1.player.ClothesList = currentClothList;
            for (var i = 0; i < Game1.player.ClothesList.Length; i++)
            {
                if (Game1.player.ClothesList[i] != null)
                {
                    Game1.player.ClothesList[i].interract(Game1.player);
                }
            }
            Game1.player.currentColumn = CurrentPlayerColumn;
            Game1.player.Health = currentHealth;
            Game1.player.MouseRotationAngle = CurrentMouseRotation;
            Game1.player.currentRow = CurrentPlayerRow;
            Game1.player.Position = CurrentPlayerPos;
        }
    }
}
