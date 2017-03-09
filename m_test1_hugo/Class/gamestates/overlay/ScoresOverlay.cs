using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using m_test1_hugo.Class.Main.outils_dev_jeu.ArmesVignette;
using m_test1_hugo.Class.Bonuses;
using m_test1_hugo.Class.Main.outils_dev_jeu.pics;
using m_test1_hugo.Class.Main.Menus.pages;
using m_test1_hugo.Class.Characters.Teams;
using Microsoft.Xna.Framework.Input;
using m_test1_hugo.Class.Main.Menus;

namespace m_test1_hugo.Class.Main.overlay
{
    public class ScoresOverlay : Sprite
    {
        SpriteFont font;
        private const int HEIGHT = 400;
        private const int WIDTH = 600;
        int posX;
        int posY;
        Rectangle sourceRectangle;

        public ScoresOverlay()
        {
            LoadContent(Game1.Content);
            Game1.graphics.IsFullScreen = true;
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("players/whiteTransparency");
            font = content.Load<SpriteFont>("font");
            foreach (Team team in Team.TeamList)
            {
                team.TeamPlayerList = new List<Player>();
            }

            posX = Game1.WindowWidth / 2 - WIDTH / 2;
            posY = Game1.WindowHeight / 2 - HEIGHT / 2;
            sourceRectangle = new Rectangle(posX, posY, WIDTH, HEIGHT);
        }
        
        public void Update(GameTime gametime)
        {
            
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Tab))
            {
                spriteBatch.Draw(texture, sourceRectangle, Color.Gray);
                int currPosY = posY + 10;
                foreach (Team team in Team.TeamList)
                {
                    foreach (Player player in team.TeamPlayerList)
                    { 
                        string score = player.Kills + " / " + player.Deaths;
                        spriteBatch.DrawString(font, player.Pseudo, new Vector2(posX + 200, currPosY), player.team._Color, 0f, Vector2.Zero, 0.2f, SpriteEffects.None, 1f);
                        spriteBatch.DrawString(font, score, new Vector2(posX + WIDTH - 200, currPosY), player.team._Color, 0f, Vector2.Zero, 0.2f, SpriteEffects.None, 1f);
                        currPosY += 50;
                    }
                    currPosY += 60;
                }
            }
        }
    }
}
