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
    class Fal : Weapon
    {
        public override Vector2 Position
        {
            get { return Holder.Center; }
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("weapons/fal");
        }

        public Fal()
        {
            this.Name = "fal";
            this.ReloadingTime = 2000;  // millisecondes
            this.RearmingTime = 10; // millisecondes
            this.MagazineSize = 21;
            this.type = "semi-auto";
            this.CurrentAmmo = MagazineSize;
            this.Damages = 22;
            this.Range = 1100;
            this.bulletSpeed = 27;
            this.bulletSprite = "ClassicBullet";
            this.MovingMalus = 0;
            this.accuracy_malus = 0.07;
            weaponSound = Game1.Content.Load<SoundEffect>("audio/weapons/" + this.Name);
        }
        public Fal(Player Holder)
        {
            this.Name = "fal";
            this.ReloadingTime = 2000;  // millisecondes
            this.RearmingTime = 10; // millisecondes
            this.MagazineSize = 21;
            this.CurrentAmmo = MagazineSize;
            this.Damages = 22;
            this.type = "semi-auto";
            this.Range = 1100;
            this.bulletSpeed = 27;
            this.bulletSprite = "ClassicBullet";
            this.MovingMalus = 0;
            this.Holder = Holder;
            this.accuracy_malus = 0.07;
            weaponSound = Game1.Content.Load<SoundEffect>("audio/weapons/" + this.Name);
        }
    }
}
