using m_test1_hugo.Class.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace m_test1_hugo.Class.Bonuses
{
    class Heal : Bonus
    {
        private int healing = 50;

        public Heal()
        {
            Bonus.BonusList.Add(this);
            Position = new Vector2(100, 100);
            name = "heal";
        }

        public override void interract(Character player)
        {
            BonusList.Remove(this);
            if (player.Health + healing > player.MaxHealth)
            {
                player.Health = player.MaxHealth;
            }
            else
            {
                player.Health += healing;
            }
            
        }

        public override void LoadContent(ContentManager content)
        {
            //texture = content.Load<Texture2D>("Bonus/Heal");
            LoadContent(content, "Bonus/Heal", 1, 1);
        }

        public override void Update(GameTime gametime)
         {
            for (var i = 0; i<Character.CharacterList.Count; i++)
            {
                Character currentPlayer = Character.CharacterList[i];

                if (this.SpriteCollision(currentPlayer.destinationRectangle))
                {
                    interract(currentPlayer);
                }
            }
        }

    }
}
