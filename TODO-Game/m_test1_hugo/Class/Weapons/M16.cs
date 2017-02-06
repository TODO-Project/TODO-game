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
    class M16: Weapon
    {
        public override Vector2 Position
        {
            get { return Holder.Center; }
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("weapons/m8a1");
        }


        public M16()
        {
            this.Name = "m8a1";
            this.ReloadingTime = 2000;  // millisecondes
            this.RearmingTime = 600; // millisecondes
            this.MagazineSize = 21;
            this.type = "rafale";
            this.CurrentAmmo = MagazineSize;
            this.Damages = 17;
            this.Range = 1000;
            this.bulletSpeed = 30;
            this.bulletSprite = "ClassicBullet";
            this.MovingMalus = 0;
            this.accuracy_malus = 0.1;
            weaponSound = Game1.Content.Load<SoundEffect>("audio/weapons/"+Name);// + this.Name);
            EndSound = Game1.Content.Load<SoundEffect>("audio/weapons/" + Name+"_end");// + this.Name);;
        }
        public M16(Player Holder)
        {
            this.Name = "m8a1";
            this.ReloadingTime = 2000;  // millisecondes
            this.RearmingTime = 600; // millisecondes
            this.MagazineSize = 21;
            this.CurrentAmmo = MagazineSize;
            this.Damages = 17;
            this.type = "rafale";
            this.Range = 1000;
            this.bulletSpeed = 30;
            this.bulletSprite = "ClassicBullet";
            this.MovingMalus = 0;
            this.Holder = Holder;
            this.accuracy_malus = 0.1;
            EndSound = Game1.Content.Load<SoundEffect>("audio/weapons/" + Name + "_end");// + this.Name);;
        }
    }
}
