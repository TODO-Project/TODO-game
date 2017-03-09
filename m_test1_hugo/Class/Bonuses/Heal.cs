using m_test1_hugo.Class.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using m_test1_hugo.Class.Main.Menus.pages;

namespace m_test1_hugo.Class.Bonuses
{
    /// <summary>
    /// Décrit le bonus de soin, qui va soigner pour un montant donné
    /// un joueur quand il passe dessus
    /// </summary>
    class Heal : Bonus
    {
        private int healing = 50;
        private SoundEffect sound;

        /// <summary>
        /// Construit un bonus de soin
        /// </summary>
        public Heal()
        {
            GamePage.BonusList.Add(this);
            Position = new Vector2(100, 100);
            name = "heal";
            LoadContent(Game1.Content);
        }

        /// <summary>
        /// Gère l'interaction entre le bonus de soin et un joueur donné
        /// </summary>
        /// <param name="player">Un joueur</param>
        public override void interract(Player player)
        {
           
               sound.Play();
            
            GamePage.BonusList.Remove(this);
            if (player.Health + healing > player.MaxHealth)
            {
                player.Health = player.MaxHealth;
            }
            else
            {
                player.Health += healing;
            }
            
        }

        /// <summary>
        /// Charge le contenu relatif au bonus de soin
        /// </summary>
        /// <param name="content">Le manager de contenu</param>
        public override void LoadContent(ContentManager content)
        {
            LoadContent(content, "Bonus/Heal", 1, 1);
            sound = content.Load<SoundEffect>("audio/bonus/heal");
        }

        /// <summary>
        /// Met à jour le bonus de soin et son interaction
        /// </summary>
        /// <param name="gametime">Le temps passé en jeu</param>
        public override void Update(GameTime gametime)
         {
            for (var i = 0; i< GamePage.PlayerList.Count; i++)
            {
                Player currentPlayer = GamePage.PlayerList[i];
                if (this.SpriteCollision(currentPlayer.destinationRectangle))
                {
                    if(currentPlayer.Health < currentPlayer.MaxHealth)
                        interract(currentPlayer);
                }
            }
        }

    }
}
