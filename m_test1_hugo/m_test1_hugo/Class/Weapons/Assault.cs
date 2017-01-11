using m_test1_hugo.Class.Main;
using m_test1_hugo.Content.weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Weapons
{
    class Assault : Weapon
    {
        public override Vector2 Position
        {
            get { return Holder.Center; }
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("weapons/assault");
        }



        public Assault()
        {
            this.Name = "assault";
            this.ReloadingTime = 2000;  // millisecondes
            this.RearmingTime = 80; // millisecondes
            this.MagazineSize = 22;
            this.CurrentAmmo = MagazineSize;
            this.Damages = 20;
            this.Range = 200;
            this.bulletSpeed = 40;
            this.bulletSprite = "ClassicBullet";
            this.MovingMalus = 0;
        }
    }
}
