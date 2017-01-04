using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Main.Menus
{
    class OptionsMenu : Menu
    {
        Button backButton;
        private int curButtonPosY { get; set; }
        public OptionsMenu()
        {
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            buttonList = new List<Button> { };

            backButton = new Button("Back");
            buttonList.Add(backButton);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.

            spriteBatch = new SpriteBatch(GraphicsDevice);
            curButtonPosY = Game1.WindowHeight / 4;

            foreach (Button button in buttonList)
            {
                button.LoadContent(Content);
                button.Position = new Vector2(Game1.WindowWidth / 2 - button.Width / 2, curButtonPosY);
                curButtonPosY += button.Height + 20;
            }

            menuBackground = Content.Load<Texture2D>("menus/BGMenu");

        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            if (backButton.leftClick())
            {
                Exit();
                using (var mainMenu = new mainMenu())
                    mainMenu.Run();
            }

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            ///////////////////////////// BUTTONS \\\\\\\\\\\\\\\
            spriteBatch.Draw(menuBackground, new Vector2(0, 0), Color.White);

            foreach (Button button in buttonList)
                button.Draw(spriteBatch);
            ///////////////////////////||||||||||\\\\\\\\\\\\\\\\
            spriteBatch.End();
        }
    }
}
