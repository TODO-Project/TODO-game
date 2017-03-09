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
    class Assault : Weapon
    {
        public override Vector2 Position
        {
            get { return Holder.Center; }
        }

        public override void LoadContent(ContentManager content)
        {
            Name = "M4A1-s";
            texture = content.Load<Texture2D>("weapons/"+Name);
        }

        public Assault()
            :base()
        {
            this.ReloadingTime = 2000;  // millisecondes
            this.RearmingTime = 115; // millisecondes
            this.MagazineSize = 25;
            this.tir = Weapon.methodeTir.auto;
            this.CurrentAmmo = MagazineSize;
            this.Damages = 18;
            this.Range = 1000;
            this.bulletSpeed = 25;
            this.bulletSprite = "ClassicBullet";
            this.MovingMalus = 0;
            this.accuracy_malus = 0.06;
        }
    }
}
