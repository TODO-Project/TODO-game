﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using m_test1_hugo.Class.Weapons;

namespace m_test1_hugo.Class.Main
{
    public abstract class Weapon : Sprite
    {

    #region Position / Sprite

        public Vector2 _canonOrigin;
        public Vector2 CanonOrigin
        {
            get { return _canonOrigin; }
            set { _canonOrigin = value; }
        }

        private Texture2D sprite;
        public Texture2D Sprite
        {
            set { sprite = value; }
            get { return sprite; }
        }

        #endregion

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public double accuracy_malus;

        private Player _holder;
        public Player Holder
        {
            get { return this._holder; }
            set
            {
                // On récupère le parent
                this._holder = value;
            }
        }

        private float range;
        public float Range
        {
            get { return range; }
            set {range = value;}
        }

        private int magazineSize;
        public int MagazineSize
        {
            get { return magazineSize;}
            set { magazineSize = value; }
        }

        // temps de rearmement
        private int rearmingTime;
        public int RearmingTime
        {
            get { return rearmingTime; }
            set { rearmingTime = value; }
        }


        private int currentAmmo;
        public int CurrentAmmo
        {
            get { return currentAmmo;}

            set { currentAmmo = value; }
        }


        #region rechargement / rearmement
        
        public bool isEmpty
        {
            get { return currentAmmo == 0; }
        }

        // Temps de recharge
        private int reloadingTime; // *1000
        public int ReloadingTime
        {
            get
            {
                return reloadingTime;
            }

            set
            {
                reloadingTime = value;
            }
        }

        // Permettra de savoir si l'on a besoin de recharger
        private bool needReloading;
        public bool NeedReloading
        {
            get { return needReloading; }
            set { needReloading = value; }
        }

        // permettra de savoir si l'on est en train de rearmer
        private bool needRearming;
        public bool NeedRearming
        {
            get { return needRearming; }
            set { needRearming = value; }
        }
        
        #endregion

        // poids de l'arme
        private int _movingMalus;
        public int MovingMalus
        {
            get { return _movingMalus; }
            set { _movingMalus = value; }
        }

              

        private int damages;
        public int Damages
        {
            get { return damages; }
            set { damages = value; }
        }

        public string bulletSprite;

        public float bulletSpeed
        {
            get;
            set;
        }

        //public abstract void Update(GameTime gametime);

        public new void Draw(SpriteBatch spritebatch)
        {
            if (Holder.CO < 0)// cote oppose holder ( voir dans les attributs, et faire un schema si besoin)
            {
                spritebatch.Draw(this.texture, Position, null, Color.White, Holder.MouseRotationAngle, new Vector2(0, this.Height / 2), -1.0f, SpriteEffects.FlipVertically, 0f); // mettre en comm pour tester ce que ca fait 
            }
            else
            {
                spritebatch.Draw(this.texture, Position, null, Color.White, Holder.MouseRotationAngle, new Vector2(0, this.Height / 2), 1.0f, SpriteEffects.None, 0f);
            }
        }

    }
}
