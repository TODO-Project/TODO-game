using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace m_test1_hugo.Class.Main.Menus
{
    class GameModeMenu : Menu
    {
        #region Fields

        SpriteBatch spriteBatch;
        Button soloButton, multiButton, testButton, backButton;

        #endregion

        #region Properties

        private int curButtonPosY { get; set; }

        #endregion

        #region Constructors

        public GameModeMenu()
        {
            Content.RootDirectory = "Content";
        }

        #endregion

        #region Methods

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            buttonList = new List<Button> { };

            soloButton = new Button("Solo");
            buttonList.Add(soloButton);
            multiButton = new Button("Multi");
            buttonList.Add(multiButton);
            testButton = new Button("Test");
            buttonList.Add(testButton);
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
                button.leftClick = false;
                button.rightClick = false;
            }

            menuBackground = Content.Load<Texture2D>("menus/BGMenu");

        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            if (soloButton.leftClick)
            {
               
            }

            if (backButton.leftClick)
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
            ms = Mouse.GetState();
            ///////////////////////////// BUTTONS \\\\\\\\\\\\\\\
            spriteBatch.Draw(menuBackground, new Vector2(0, 0), Color.White);

            foreach (Button button in buttonList)
                button.Draw(spriteBatch);
            ///////////////////////////||||||||||\\\\\\\\\\\\\\\\
            spriteBatch.End();
        }

        #endregion
    }
}
