using m_test1_hugo.Class.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace m_test1_hugo.Class.Bonuses
{
    class SpeedBuff : Bonus
    {
        public bool chronoStarted;

        DateTime chrono;
        int Duration = 3000;
        int moveSpeedBonus = 4;

        Player currentPlayer;

        public override void interract(Player player)
        {
            
        }

        public SpeedBuff()
        {
            Bonus.BonusList.Add(this);
            Position = new Vector2(700, 100);
            name = "speedBuff";
            currentRow = 0;
        }



        public override void LoadContent(ContentManager content)
        {
            LoadContent(content, "Bonus/speedBuff", 2, 1);
        }

        public override void Update(GameTime gametime)
        {
            if (!chronoStarted)
            {
                for (var i = 0; i < Player.CharacterList.Count; i++)
                {
                    currentPlayer = (Player)Player.CharacterList[i];
                    if (SpriteCollision(currentPlayer.destinationRectangle))
                    {
                        chrono = DateTime.Now;
                        chronoStarted = true;
                        currentPlayer.weapon.MovingMalus -= moveSpeedBonus;
                        currentRow = 1;
                    }
                }
            }
            if(chronoStarted && DateTime.Now > chrono.AddMilliseconds(Duration))
            {
                //
                BonusList.Remove(this);
            }
        }
    }
}
