using m_test1_hugo.Class.Main;
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
    class shotgun : Weapon
    {
        public override Vector2 Position
        {
            get { return Holder.Center; }
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("weapons/shotgun");
        }



        public shotgun()
        {
            this.Name = "shotgun";
            this.ReloadingTime = 2000;  // millisecondes
            this.RearmingTime = 1000; // millisecondes
            this.MagazineSize = 3;
            this.CurrentAmmo = MagazineSize;
            this.Damages = 45;
            this.Range = 250;
            this.bulletSpeed = 40;
            this.bulletSprite = "ClassicBullet";
            this.MovingMalus = 0;
        }
    }
}
