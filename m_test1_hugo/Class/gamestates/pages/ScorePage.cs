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
using System.IO;

namespace m_test1_hugo.Class.Main.Menus.pages
{
    /// <summary>
    /// GameState : Menu Affichage des scores
    /// </summary>
    class ScorePage :MenuPage
    {
        private List<string> scores = new List<string>();
        SpriteFont font;

        public ScorePage()
        {
            buttons.Add(new Button("Back"));
            Console.WriteLine("ScorePage");
            string line;
            
            StreamReader reader;
            if (File.Exists(@"../../../../scores/scores.txt")) 
            {
                reader = new StreamReader(@"../../../../scores/scores.txt");
                while ((line = reader.ReadLine()) != null)
                {
                    scores.Add(line);
                }
                font = Game1.Content.Load<SpriteFont>("font");
            }
        }

        public override void Update()
        {

        }

        public override MenuPage Action()
        {
            if (buttons[0].leftClick())
                return new MainPage();
            else
                return null;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            var currentY = 50;
            var currentX = Game1.WindowWidth/2-150; // largeur d'un bouton : 300
            foreach (Button button in buttons)
            {
                button.Position = new Vector2(currentX, currentY);
                currentY += 130;
                button.Draw(spriteBatch);
            }

            foreach (string score in scores)
            {
                currentY += 80;
                Vector2 Position = new Vector2(Game1.WindowWidth / 2, currentY);
                spriteBatch.DrawString(font, score, Position, Color.White, 0f, font.MeasureString(score)/2, 0.3f, SpriteEffects.None, 1f );
            }
                
        }
    }
}
