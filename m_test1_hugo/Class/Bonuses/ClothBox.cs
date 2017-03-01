using entrainementProjet1.Class.Main;
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
    /// <summary>
    /// Décrit une boîte d'habits, qui peut être ouverte par 
    /// le joueur pour obternir un habit aléatoire
    /// </summary>
    class ClothBox:RandomBox
    {
        /// <summary>
        /// Récupère et définit la durée du chronomètre
        /// </summary>
        public override int chronoDuration { get; set; }

        /// <summary>
        /// Récupère et définit le booléen d'ouverture de la boîte
        /// </summary>
        public override bool isOpen { get; set; }

        /// <summary>
        /// Récupère et définit le booléen du message d'ouverture
        /// </summary>
        public override bool pressButtonMsg { get; set; }

        /// <summary>
        /// Récupère et définit le booléen de récupération d'habit aléatoire
        /// </summary>
        public override bool FoundRandom { get; set; }

        /// <summary>
        /// Récupère et définit le booléen de validation
        /// </summary>
        public override bool Validated { get; set; }

        /// <summary>
        /// Récupère et définit le booléen de début de chronomètre
        /// </summary>
        public override bool TimerStarted { get; set; }

        /// <summary>
        /// Récupère et définit le booléen de l'activation de la musique
        /// </summary>
        public override bool musicPlayed { get; set; }

        /// <summary>
        /// Récupère et définit l'image de l'habit
        /// </summary>
        public ClothPic clothPic;

        /// <summary>
        /// Récupère et définit le son de l'ouverture
        /// </summary>
        public override SoundEffect sound { get; set; }

        /// <summary>
        /// Récupère et définit le chronomètre
        /// </summary>
        public override DateTime chrono { get; set; }
        

        Player currentPlayer = GamePage.player;
        Cloth randomCloth;

        /// <summary>
        /// Construit une boîte d'habit selon sa position
        /// </summary>
        /// <param name="Position">La position de l'habit</param>
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
        /// Récupère un habit aléatoire
        /// </summary>
        public void RandomObject()
        {
            Random rnd = new Random();
            int RandInt = rnd.Next(Cloth.list.Count<Cloth>());
            randomCloth = Cloth.list[RandInt];
        }

        /// <summary>
        /// Gère l'interaction entre la boîte et un joueur donné
        /// </summary>
        /// <param name="player">Un joueur</param>
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

        /// <summary>
        /// Charge le contenu nécéssaire à la boîte d'habits
        /// </summary>
        /// <param name="content">Le manager de contenu</param>
        public override void LoadContent(ContentManager content)
        {
            LoadContent(content, "Bonus/magicBox", 2, 1);
        }

        /// <summary>
        /// Procédure de mise à jour de la boîte et de son ouverture
        /// </summary>
        /// <param name="gametime">Le temps passé en jeu</param>
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
                        Console.WriteLine(chronoDuration);
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
