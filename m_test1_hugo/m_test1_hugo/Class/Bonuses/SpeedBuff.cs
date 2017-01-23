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
        int Duration = 10;
        int moveSpeedBonus = 4;

        Player currentPlayer;

        public SpeedBuff()
        {
            Bonus.BonusList.Add(this);
            Position = new Vector2(700, 100);
            name = "speedBuff";
            currentRow = 0;
        }

        public override void interract(Player character)
        {
            chrono = DateTime.Now;
            chronoStarted = true;
            currentPlayer.MoveSpeed += moveSpeedBonus;
            currentRow = 1;
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
                        interract(currentPlayer);
                    }
                }
            }
            else
            {
                if (DateTime.Now > chrono.AddSeconds(Duration))
                {
                    currentPlayer.MoveSpeed = currentPlayer.classe.MoveSpeed - currentPlayer.weapon.MovingMalus;
                    BonusList.Remove(this);
                }
            }
        }
    }
}
