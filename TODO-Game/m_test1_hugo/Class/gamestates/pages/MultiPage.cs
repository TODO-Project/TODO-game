using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace m_test1_hugo.Class.Main.Menus.pages
{
    class MultiPage:MenuPage
    {
        public MultiPage()
        {
            buttons.Add(new Button("Create server"));
            buttons.Add(new Button("Join Server"));
            buttons.Add(new Button("Back"));
            Console.WriteLine(buttons[0].Text);
        }

        public override void Update()
        {
            
        }

        public override MenuPage Action()
        {
            if (buttons[0].leftClick())
                return new CreateServ();
            else if (buttons[2].leftClick())
                return new MainPage();
            else
                return null;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var currentY = 50;
            var currentX = Game1.WindowHeight / 2 + 150; // largeur d'un bouton : 300
            foreach (Button button in buttons)
            {
                button.Position = new Vector2(currentX, currentY);
                currentY += 130;
                button.Draw(spriteBatch);
            }    
        }
    }
}
