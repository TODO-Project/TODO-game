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

namespace m_test1_hugo.Class.Bonuses
{
    class ClothBox:Bonus
    {
        public bool isOpen = false;

        DateTime Chrono;
        Cloth randomCloth;
        Player currentPlayer = Game1.player;

        public ClothBox()
        {
            name = "clothBox";
            BonusList.Add(this);
            currentRow = 1;
        }

        public void SelectRandomCloth()
        {
            Random rnd = new Random();
            //int RandInt = rnd.Next(Cloth);
        }

    public override void interract(Player player)
        {
            
        }
        public override void LoadContent(ContentManager content)
        {
            LoadContent(content, "Bonus/magicBox", 2, 1);


        }

        public override void Update(GameTime gametime)
        {
            if (this.SpriteCollision(currentPlayer.destinationRectangle))
            {
                //this.pressButtonMsg = true;
                if (Game1.kb.IsKeyDown(Keys.E) && !isOpen)
                    isOpen = true;
            }
            else
            {
                //this.pressButtonMsg = false;
            }
            if (isOpen)
            {
                currentRow = 0;
            }
        }
    }
}
