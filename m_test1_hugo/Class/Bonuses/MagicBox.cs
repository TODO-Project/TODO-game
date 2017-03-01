﻿using m_test1_hugo.Class.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using m_test1_hugo.Class.Main.outils_dev_jeu.Affects;
using m_test1_hugo.Class.Main.outils_dev_jeu.ArmesVignette;
using Microsoft.Xna.Framework.Audio;
using m_test1_hugo.Class.Main.Menus.pages;

namespace m_test1_hugo.Class.Bonuses
{
    /// <summary>
    /// Décrit une boîte d'arme, qui contient une arme aléatoire,
    /// donnée au joueur après interaction.
    /// </summary>
    class MagicBox : RandomBox
    {
        #region attributs
        public override int chronoDuration { get;  set; }
        public override bool isOpen { get; set; }
        public override bool pressButtonMsg { get; set; }
        public override bool FoundRandom { get; set; }
        public override bool Validated { get; set; }
        public override bool TimerStarted { get; set; }
        public override bool musicPlayed { get; set; }

        public override SoundEffect sound { get; set; }
        public override DateTime chrono { get; set; }

        private int currentFake = 0;
        private int tempoDuration = 800; // milliseconds
        #endregion

        DateTime tempo;
        Weapon randomWeapon;
        WeaponPic weaponPic, fakewPic;
        Player currentPlayer = GamePage.player;
        public bool tempoStarted = false;

        /// <summary>
        /// Définit l'interaction entre la boîte d'arme et un joueur
        /// </summary>
        /// <param name="player">Un joueur</param>
        public override void interract(Player player)
        {
            CharacterAffect.WeaponChange(player, randomWeapon);
            GamePage.PicList.Remove(weaponPic);
            GamePage.BonusList.Remove(this);
        }

        /// <summary>
        /// Charge le contenu relatif à la boîte d'arme
        /// </summary>
        /// <param name="content">Le manager de contenu</param>
        public override void LoadContent(ContentManager content)
        {
            LoadContent(content, "Bonus/magicBox", 2, 1);
            sound= content.Load<SoundEffect>("audio/bonus/magicbox");
        }

        /// <summary>
        /// Construit une boîte d'arme selon sa position
        /// </summary>
        /// <param name="Position">La position de la boîte</param>
        public MagicBox(Vector2 Position)
            :base(Position)
        {
            LoadContent(Game1.Content);
            name = "magicBox";
            GamePage.BonusList.Add(this);
            currentRow = 1;
            chronoDuration = 10;
        }

        /// <summary>
        /// Récupère une arme aléatoire
        /// </summary>
        public void RandomObject()
        {
            Random rnd = new Random();
            int RandInt = rnd.Next(Weapon.List.Count<Weapon>());
            randomWeapon = Weapon.List[RandInt];
            randomWeapon.Holder = currentPlayer;
            weaponPic = new WeaponPic(randomWeapon, Position);
            chrono = DateTime.Now;
            TimerStarted = true;
            FoundRandom = true;
        }

        /// <summary>
        /// Procédure de mise à jour de la boîte et de son contenu
        /// </summary>
        /// <param name="gameTime">Le temps passé en jeu</param>
        public override void Update(GameTime gameTime)
        {
            if (this.SpriteCollision(currentPlayer.destinationRectangle))
            {
                this.pressButtonMsg = true;
                if (currentPlayer.Controls.Use && !isOpen)
                    isOpen = true;
            }
            else
            {
                this.pressButtonMsg = false;
            }
            if (isOpen)
            {
                if (!musicPlayed)
                {
                    sound.Play();
                    musicPlayed = true;
                }
                this.pressButtonMsg = false;
                currentRow = 0; // sprite ouvert
                if (!FoundRandom) // on va afficher les armes dans l'ordre 
                {

                    if (!tempoStarted)
                    {
                        tempo = DateTime.Now;
                        tempoStarted = true;
                        fakewPic = new WeaponPic(Weapon.List[currentFake], Position);
                    }
                    else
                    {
                        if (currentFake < Weapon.List.Length)
                        {
                            if (DateTime.Now > tempo.AddMilliseconds(tempoDuration))
                            {
                                tempoStarted = false;
                                GamePage.PicList.Remove(fakewPic);
                                currentFake++;
                                if (currentFake >= Weapon.List.Length) // si on a affiche toutes les armes, on en prend une vraie au hasard
                                {
                                    RandomObject();
                                }
                            }
                        }
                    }
                }
                else if (SpriteCollision(currentPlayer.destinationRectangle)) // si le joueur est sur la boite et qu'il peut prendre l'arme
                {
                    weaponPic.takeMsg = true;
                    if (currentPlayer.Controls.Use)
                    {
                        interract(currentPlayer);
                    }
                }

                if (TimerStarted)
                {
                    if (DateTime.Now > chrono.AddSeconds(chronoDuration))
                    {
                        GamePage.PicList.Remove(weaponPic);
                        GamePage.BonusList.Remove(this);
                    }
                    if (!SpriteCollision(currentPlayer.destinationRectangle))
                    {
                        weaponPic.takeMsg = false;
                    }
                }
            }
        }
    }
}
