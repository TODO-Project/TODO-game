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
            Name = "M16";
            texture = content.Load<Texture2D>("weapons/"+Name);
        }


        public M16()
            :base()
        {
            this.ReloadingTime = 2000;  // millisecondes
            this.RearmingTime = 600; // millisecondes
            this.MagazineSize = 21;
            this.tir = methodeTir.rafale;
            this.CurrentAmmo = MagazineSize;
            this.Damages = 17;
            this.Range = 1000;
            this.bulletSpeed = 15;
            this.bulletSprite = "ClassicBullet";
            this.MovingMalus = 0;
            this.accuracy_malus = 0.1;
            EndSound = Game1.Content.Load<SoundEffect>("audio/weapons/" + Name + "_end");// + this.Name);;
        }
    }
}
