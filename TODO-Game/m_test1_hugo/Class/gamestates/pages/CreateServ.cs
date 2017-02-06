using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using m_test1_hugo.Class.Main.Menus.tools;
using Microsoft.Xna.Framework.Input;

namespace m_test1_hugo.Class.Main.Menus.pages
{
    class CreateServ:MenuPage
    {
        KeyboardState kb;
        private TextInput PseudoInput;
        public CreateServ()
        {
            buttons.Add(new Button("Create Game"));
            buttons.Add(new Button("Back"));
            PseudoInput = new TextInput("Ton pseudo :", 16);
            PseudoInput.Position = new Vector2(400, 100);
            Console.WriteLine(buttons[0].Text);
        }


        public override void Update()
        {

        }

        public override MenuPage Action()
        {
            if (buttons[0].leftClick() || kb.IsKeyDown(Keys.Enter))
            {
                GamePage.Pseudo = PseudoInput.Text;
                return new GamePage();
            }
                
            else
                return null;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            kb = Keyboard.GetState();
            var currentY = 50;
           // var currentX = Game1.WindowHeight / 2 - 150; // largeur d'un bouton : 300
            foreach (Button button in buttons)
            {
                button.Position = new Vector2(80, currentY);
                currentY += 130;
                button.Draw(spriteBatch);
                PseudoInput.Draw(spriteBatch);
            }   
        }
    }
}
