using entrainementProjet1.Class.Main;
using m_test1_hugo.Class.Main;
using m_test1_hugo.Class.Main.outils_dev_jeu.Affects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.clothes
{
    /// <summary>
    /// T-shirt, augmentant la puissance du personnage
    /// </summary>
    public class Shirt : Cloth
    {
        #region attributs
        private int bonus;
        public override int Bonus
        {
            get
            {
                return bonus;
            }
            set
            {
                this.bonus = value;
            }
        }

        #endregion

        // public delegate void Bonus(Player player, int bonus);

        public override Vector2 Position
        {
            get { return new Vector2(7, 95); }
        }

        public Shirt(string name, int damageBonus)
            :base(name, damageBonus)
        {
            this.Bonus = damageBonus;
            this.clothName = name;
            LoadContent(Game1.Content);
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("clothes/shirts/" + clothName);
        }

        public override void interract(Player player)
        {
            player.ClothesList[0].TakeOff(player);
            CharacterAffect.increaseDamages(player, bonus);
        }

        public override void TakeOff(Player player)
        {
            if (player.ClothesList[0] != null)
            {
                CharacterAffect.decreaseDamages(player, bonus);
                player.ClothesList[0] = null;
            }
        }
    }
}

/*public override void interract(Player player)  POUR LES T-SHIRT
{
    player.ClothesList[0] = this;

    player.MaxHealth = player.classe.Health + healthBonus;
    player.Health = player.MaxHealth;
*/