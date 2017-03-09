using m_test1_hugo.Class.Main;
using m_test1_hugo.Class.Main.interfaces;
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
    class Minigun : Weapon
    {
        public override Vector2 Position
        {
            get { return Holder.Center; }
        }

        public override void LoadContent(ContentManager content)
        {
            Name = "minigun";
            texture = content.Load<Texture2D>("weapons/"+Name);
        }

        public Minigun()
            :base()
        { 
            this.ReloadingTime = 4000;  // millisecondes
            this.RearmingTime = 20 ; // millisecondes
            this.MagazineSize = 95;
            this.CurrentAmmo = MagazineSize;
            this.Damages = 8;
            this.tir = methodeTir.auto;
            this.Range = 800;
            this.bulletSpeed = 20;
            this.bulletSprite = "ClassicBullet";
            this.MovingMalus = 2;
            this.accuracy_malus = 0.1;
        }
    }
}
