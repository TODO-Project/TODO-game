﻿using entrainementProjet1.Class.Main;
using m_test1_hugo.Class.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using m_test1_hugo.Class.clothes;
using m_test1_hugo.Class.Main.outils_dev_jeu.pics;
using Microsoft.Xna.Framework.Audio;
using m_test1_hugo.Class.Main.Menus.pages;

namespace m_test1_hugo.Class.Bonuses
{
    class ClothBox:RandomBox
    {
        public override int chronoDuration { get; set; }
        public override bool isOpen { get; set; }
        public override bool pressButtonMsg { get; set; }
        public override bool FoundRandom { get; set; }
        public override bool Validated { get; set; }
        public override bool TimerStarted { get; set; }
        public override bool musicPlayed { get; set; }

        public ClothPic clothPic;

        public override SoundEffect sound { get; set; }
        public override DateTime chrono { get; set; }
        

        Player currentPlayer = GamePage.player;
        Cloth randomCloth;

        public ClothBox(Vector2 Position)
            :base(Position)
        {
            name = "clothBox";
            GamePage.BonusList.Add(this);
            currentRow = 1;
            chronoDuration = 30;
            LoadContent(Game1.Content);
        }

        /// <summary>
        /// methode qui genere un vetement aleatoire 
        /// </summary>
        public void RandomObject()
        {
            Random rnd = new Random();
            int RandInt = rnd.Next(Cloth.list.Count<Cloth>());
            randomCloth = Cloth.list[RandInt];
        }

        /// <summary>
        /// assignation du vetement au joueur
        /// </summary>
        /// <param name="player"></param>
        public override void interract(Player player)
        {
            if(randomCloth is Boots)
            {
                player.ClothesList[2] = randomCloth;
            }
            else if(randomCloth is Shirt)
            {
                player.ClothesList[0] = randomCloth;
            }
            else if (randomCloth is Pant)
            {
                player.ClothesList[1] = randomCloth;
                player.ClothesList[1].interract(player);
            }
            GamePage.BonusList.Remove(this);
            GamePage.PicList.Remove(clothPic);
        }

        public override void LoadContent(ContentManager content)
        {
            LoadContent(content, "Bonus/magicBox", 2, 1);
        }

        public override void Update(GameTime gametime)
        {
            if (SpriteCollision(currentPlayer.destinationRectangle))
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
                if (!TimerStarted)
                {
                    RandomObject();
                    clothPic = new ClothPic(randomCloth, Position);
                    TimerStarted = true;
                    chrono = DateTime.Now;
                }
                else
                {
                    if(DateTime.Now > chrono.AddSeconds(chronoDuration))
                    {
                        ////console.writeLine(chronoDuration);
                        GamePage.BonusList.Remove(this);
                    }
                }

                currentRow = 0; // sprite ouvert
                pressButtonMsg = false;
                
                if (!currentPlayer.Controls.Use)
                    Validated = true;
                if (Validated && SpriteCollision(currentPlayer.destinationRectangle))
                {
                    pressButtonMsg = true;
                    if(currentPlayer.Controls.Use)
                    {
                        interract(currentPlayer);
                    }
                }
            }
        }
    }
}
