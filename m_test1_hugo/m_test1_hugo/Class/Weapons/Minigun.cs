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
    class Minigun : Weapon
    {
        public override Vector2 Position
        {
            get { return Holder.Center; }
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("weapons/minigun");
        }

        public Minigun()
        {
            this.Name = "minigun";
            this.ReloadingTime = 4000;  // millisecondes
            this.RearmingTime = 20 ; // millisecondes
            this.MagazineSize = 80;
            this.CurrentAmmo = MagazineSize;
            this.Damages = 8;
            this.Range = 200;
            this.bulletSpeed = 80;
            this.bulletSprite = "ClassicBullet";
            this.MovingMalus = 2;
            this.accuracy_malus = 0.2;
        }
    }
}
