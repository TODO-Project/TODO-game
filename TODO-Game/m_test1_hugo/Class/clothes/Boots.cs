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
    class Boots:Cloth
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
            get { return new Vector2(9, 257); }
        }

        public Boots(string name, int moveSpeedBonus)
            :base(name, moveSpeedBonus)
        {
            this.Bonus = moveSpeedBonus;
            this.clothName = name;
            LoadContent(Game1.Content);
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("clothes/boots/" + clothName);
        }

        public override void interract(Player player)
        {
            CharacterAffect.increaseMoveSpeed(player, 4);
            player.ClothesList[2] = this;
        }

        public override void TakeOff(Player player)
        {
            if (player.ClothesList[2] != null)
            {
                CharacterAffect.decreaseMoveSpeed(player, 4);
                player.ClothesList[2] = null;
            }
        }
    }
}
