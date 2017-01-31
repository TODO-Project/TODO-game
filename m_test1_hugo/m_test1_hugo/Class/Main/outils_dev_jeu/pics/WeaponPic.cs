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

        public override string textureName
        {
            get;
            set;
        }

        public override bool takeMsg
        {
            get;
            set;
        }

        public WeaponPic(Weapon weapon, Vector2 position)
            :base(position)
        {
            textureName = weapon.Name;
            PicList.Add(this);
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("weapons/"+textureName);
        }
    }
}
