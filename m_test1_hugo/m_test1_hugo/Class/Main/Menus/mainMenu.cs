using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace m_test1_hugo.Class.Main.Menus
{
    public class mainMenu : Menu
    {
        #region Fields

        SpriteBatch spriteBatch;
        Button gameButton, optionButton, exitButton, scoresButton;

        #endregion

        #region Properties

        private int curButtonPosY { get; set; }

        #endregion

        #region Constructor

        public mainMenu()
        { 
            Content.RootDirectory = "Content";
        }

        #endregion

        #region Methods

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            buttonList = new List<Button> { };
            gameButton = new Button("Game");
            optionButton = new Button("Options");
            scoresButton = new Button("Scores");
            exitButton = new Button("Exit");
            buttonList.Add(gameButton);
            buttonList.Add(optionButton);
            buttonList.Add(scoresButton);
            buttonList.Add(exitButton);

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
                button.Position = new Vector2(Game1.WindowWidth / 2 - button.Width / 2, curButtonPosY  );
                curButtonPosY += button.Height + 20;
                button.leftClick = false;
                button.rightClick = false;
            }

            menuBackground = Content.Load<Texture2D>("menus/BGMenu");

        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
           

            if (gameButton.leftClick)
            {
                Exit();
                using (var gameModeMenu = new GameModeMenu())
                    gameModeMenu.Run();
            }

            if (exitButton.leftClick)
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

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
