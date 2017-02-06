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
    class WeaponPic : Sprite
    {
        public static List<WeaponPic> WeaponPicList = new List<WeaponPic>(); 

        private string textureName;
        public bool takeWeaponMsg = false;
        

        public WeaponPic(Weapon weapon, Vector2 position)
        {
            this.Position = new Vector2(position.X, position.Y-30 );
            textureName = weapon.Name;
            WeaponPicList.Add(this);
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("weapons/"+textureName);
        }
    }
}
