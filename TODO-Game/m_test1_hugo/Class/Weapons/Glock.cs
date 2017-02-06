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
    class Glock : Weapon
    {
        public override Vector2 Position
        {
            get { return Holder.Center; }
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("weapons/glock");
        }

        public Glock()
        {
            this.Name = "glock";
            this.ReloadingTime = 1200;  // millisecondes
            this.RearmingTime = 95; // millisecondes
            this.MagazineSize = 10;
            this.type = "semi-auto";
            this.CurrentAmmo = MagazineSize;
            this.Damages = 13;
            this.Range = 800;
            this.bulletSpeed = 50;
            this.bulletSprite = "ClassicBullet";
            this.MovingMalus = -1;
            this.accuracy_malus = 0.07;
            weaponSound = Game1.Content.Load<SoundEffect>("audio/weapons/" + this.Name);
        }
        public Glock(Player Holder)
        {
            this.Name = "glock";
            this.ReloadingTime = 1200;  // millisecondes
            this.RearmingTime = 95; // millisecondes
            this.MagazineSize = 10;
            this.CurrentAmmo = MagazineSize;
            this.Damages = 13;
            this.type = "semi-auto";
            this.Range = 800;
            this.bulletSpeed = 50;
            this.bulletSprite = "ClassicBullet";
            this.MovingMalus = -1;
            this.Holder = Holder;
            this.accuracy_malus = 0.07;
            weaponSound = Game1.Content.Load<SoundEffect>("audio/weapons/" + this.Name);
        }
    }
}
