using m_test1_hugo.Class.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Weapons
{
    class Shotgun : Weapon
    {
        public override Vector2 Position
        {
            get { return Holder.Center; }
        }

        public override void LoadContent(ContentManager content)
        {
            Name = "shotgun";
            texture = content.Load<Texture2D>("weapons/"+Name);
        }

        public Shotgun()
        {
            this.ReloadingTime = 2000;  // millisecondes  2000
            this.RearmingTime = 1000; // millisecondes   1000
            this.MagazineSize = 4;
            this.CurrentAmmo = MagazineSize;
            this.Damages = 45;
            this.tir = methodeTir.semiAuto;
            this.Range = 470;
            this.bulletSpeed = 40;
            this.bulletSprite = "ClassicBullet";
            this.MovingMalus = 0;
            weaponSound = Game1.Content.Load<SoundEffect>("audio/weapons/" + this.Name);
        }
    }
}
