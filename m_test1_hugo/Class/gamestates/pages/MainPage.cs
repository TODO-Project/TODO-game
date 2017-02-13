using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using m_test1_hugo.Class.Main.overlay;
using m_test1_hugo.Class.Tile_Engine;
using Microsoft.Xna.Framework.Input;
using m_test1_hugo.Class.ControlLayouts;
using m_test1_hugo.Class.Main.outils_dev_jeu.ControlLayouts;
using m_test1_hugo.Class.Characters.Teams;
using m_test1_hugo.Class.Main.Menus.tools;
using m_test1_hugo.Class.gamestates.pages;

namespace m_test1_hugo.Class.Main.Menus.pages
{
    class MainPage:MenuPage
    {
        private Texture2D bgTexture = Game1.Content.Load<Texture2D>("bg");
        private Vector2 bgPosition = Vector2.Zero;

        public MainPage()
        {
            buttons.Add(new Button("Multiplayer"));
            buttons.Add(new Button("Solo"));
            buttons.Add(new Button("Options"));
            buttons.Add(new Button("Scores"));
            buttons.Add(new Button("Exit"));
        }

        public override void Update()
        {

        }

        public override MenuPage Action()
        {
            if (buttons[0].leftClick())
                return new MultiPage();
            else if (buttons[3].leftClick())
                return new ScorePage();
            else if (buttons[4].leftClick())
                return new ExitPage();
            else
                return null;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bgTexture, bgPosition, Color.White);
            var currentY = 50;
            var currentX = Game1.WindowWidth/2-150; // largeur d'un bouton : 300
            foreach (Button button in buttons)
            {
                button.Position = new Vector2(currentX, currentY);
                currentY += 130;
                button.Draw(spriteBatch);
            }
        }
    }
}
