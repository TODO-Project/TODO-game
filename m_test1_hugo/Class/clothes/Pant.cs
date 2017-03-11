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
    /// Pantalon, augmentant la sante
    /// </summary>
    class Pant :Cloth
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

        public override Vector2 Position
        {
            get { return new Vector2(20, 175); }
        }

        public Pant(string name, int healthBonus)
            :base(name, healthBonus)
        {
            this.Bonus = healthBonus;
            this.clothName = name;
            LoadContent(Game1.Content);
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("clothes/pants/" + clothName);
        }

        public override void interract(Player player)
        {
            //player.ClothesList[1].TakeOff(player);
            player.ClothesList[1] = this;
            //CharacterAffect.increaseHealth(player, Bonus);
        }

        public override void TakeOff(Player player)
        {
            if (player.ClothesList[1] != null)
            {
                //CharacterAffect.decreaseHealth(player, Bonus);
                player.ClothesList[1] = null;
            }
        }
    }
}
