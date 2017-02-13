using m_test1_hugo.Class.Main.Menus.pages;
using m_test1_hugo.Class.Main.outils_dev_jeu.pics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Main.outils_dev_jeu.ArmesVignette
{
    class WeaponPic : Pics
    {


        private Weapon weapon;

        public override bool takeMsg
        {
            get;
            set;
        }

        public override string textureName
        {
            get
            {
                return "";
            }

            set
            {
                textureName = "";
            }
        }

        public WeaponPic(Weapon weapon, Vector2 position)
            :base(position)
        {
            this.weapon = weapon;
            GamePage.PicList.Add(this);
            LoadContent(Game1.Content);
        }

        public override void LoadContent(ContentManager content)
        {
            if(weapon != null)
                texture = content.Load<Texture2D>("weapons/"+weapon.Name);
        }
    }
}
