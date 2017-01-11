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
        }

        public override void interract(Player player)
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
            texture = content.Load<Texture2D>("Bonus/Heal");
        }


    }
}
