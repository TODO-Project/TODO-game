using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace m_test1_hugo.Class.Main
{
    public abstract class Weapon : Sprite
    {

        private string name;
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        private Texture2D sprite;
        public Texture2D Sprite
        {
            set { sprite = value;}
            get { return sprite; }
        }

        private float range;
        public float Range
        {
            get
            {
                return range;
            }

            set
            {
                range = value;
            }
        }

        private int magazineSize;
        public int MagazineSize
        {
            get
            {
                return magazineSize;
            }

            set
            {
                magazineSize = value;
            }
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
            get
            {
                return currentAmmo;
            }

            set
            {
                currentAmmo = value;
            }
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

        // Permettra de savoir si l'on est en train de recharger
        private bool reloading;
        public bool Reloading
        {
            get { return reloading; }
            set { reloading = value; }
        }

        // permettra de savoir si l'on est en train de rearmer
        private bool rearming;
        public bool Rearming
        {
            get { return rearming; }
            set { rearming = value; }
        }

        private DateTime initReloading;
        public DateTime InitReloading
        {
            get { return initReloading; }
            set { initReloading = value; }
        }

        private DateTime initRearming;
        public DateTime InitRearming
        {
            get { return initRearming; }
            set { initRearming = value; }
        }

        // poids de l'arme
        private int weight;
        public int Weight;

        //private Sprite sprite;

        // Methodes
        public bool isEmpty()
        {
            return currentAmmo == 0;
        }

        private int damages;
        public int Damages
        {
            get { return damages; }
            set { damages = value; }
        }

        

        // Permettra de savoir si l'on est en train de recharger ou non
        private bool CurrentlyReloading()
        {

            if (this.Reloading)
            {
                // On récupère la date
                DateTime now = DateTime.Now;

                // On regarde si l'on a dépasé le temps de chargement
                if (now > this.InitReloading.AddMilliseconds(this.ReloadingTime))
                {
                    // On indique que l'on a fini de chargé
                    this.Reloading = false;

                    //On recharge le chargeur 
                    this.CurrentAmmo = this.MagazineSize;

                    return true;
                }
                else
                {
                    Console.WriteLine("Reloading... you must wait !");
                }
            }
            return false;
        }

        public bool CurrentlyRearming()
        {
            if (this.Rearming)
            {
                DateTime now = DateTime.Now;
                if (now > this.InitRearming.AddMilliseconds(this.RearmingTime))
                {
                    this.Rearming = false;
                    return true;
                }
                else
                {
                    Console.WriteLine("Rearming... Wait");
                }
            }
            return false;
        }


        public void shoot()
        {

            if (!CurrentlyReloading())
            {
                if (!this.isEmpty())
                {
                    /* TO DO --  instanciation d'une balle qui part de (positionX_Canon, positionY_Canon) jusqu'à un point B + ou - un chiffre aléatoire lié à la précision de l'arme,
                    en fonction de l'angle de tir et de la portée (résoudre une équation avec chasles) ou pythagore */
                    if (!CurrentlyRearming())
                    {

                        if (!Rearming)
                        {
                            Console.WriteLine("pan ! : il reste " + (CurrentAmmo - 1).ToString() + " munitions");
                            InitRearming = DateTime.Now;
                            this.CurrentAmmo--;
                            this.Rearming = true;
                        }
                    }
                }
                if (this.isEmpty())
                {
                    if (!Reloading)
                    {
                        Console.WriteLine("clic !");
                        // On enregistre la date et l'heure du rechargement
                        InitReloading = DateTime.Now;

                        // On indique que l'on recharge
                        this.Reloading = true;
                    }
                }
            }
        }
    }
}
