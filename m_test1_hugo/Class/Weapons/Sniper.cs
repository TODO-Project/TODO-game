﻿using m_test1_hugo.Class.Main;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace m_test1_hugo.Class.Weapons
{
    public class Sniper : Weapon
    {
      
        // Virer le new et mettre override pour écraser le getter
        public override Vector2 Position
        {
            get { return Holder.Center; }
        }

        public override void LoadContent(ContentManager content)
        {
            Name = "sniper";
            texture = content.Load<Texture2D>("weapons/"+Name);
        }

        public Sniper()
            :base()
        {
            this.ReloadingTime = 2500;  // millisecondes
            this.RearmingTime = 1200; // millisecondes
            this.MagazineSize = 5;
            this.CurrentAmmo = MagazineSize;
            this.Damages = 100;
            this.tir = methodeTir.semiAuto;
            this.Range = 1000;
            this.bulletSpeed = 60;
            this.bulletSprite = "ClassicBullet";
            this.MovingMalus = 0;
            this.accuracy_malus = 0;
        }

        public Sniper(Player Holder)
            :this()
        {
            this.Holder = Holder;
        }
    }
}