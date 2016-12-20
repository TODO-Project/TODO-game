using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace m_test1_hugo.Class.Main
{
    public class InputHandler : Microsoft.Xna.Framework.GameComponent
    {
        #region Fields

        static KeyboardState keyboardState;      // État du clavier au moment présent
        static KeyboardState lastKeyboardState;  // État du clavier à la frame précédente

        #endregion

        #region Properties

        public static KeyboardState KeyboardState
        {
            get { return KeyboardState; }
        }

        public static KeyboardState LastKeyboardState
        {
            get { return lastKeyboardState; }
        }



        #endregion

        #region Constructor

        public InputHandler(Game game) : base(game)
        {
            keyboardState = Keyboard.GetState();
        }

        #endregion

        #region Methods (XNA)

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            lastKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            base.Update(gameTime);
        }

        #endregion

        #region Methods (General)

        /// <summary>
        /// Nettoie le tampon des entrées clavier (met à false KeyPressed et KeyReleased)
        /// </summary>
        public static void FlushKeyboardBuffer()
        {
            lastKeyboardState = keyboardState;
        }

        /// <summary>
        /// Indique si une touche du clavier a été relâchée
        /// </summary>
        /// <param name="key">Une touche du clavier</param>
        /// <returns>Retourne true si la touche a été relâchée, false sinon</returns>
        public static bool KeyReleased(Keys key)
        {
            return (keyboardState.IsKeyUp(key) && lastKeyboardState.IsKeyUp(key)); 
        }

        /// <summary>
        /// Indique si une touche du clavier a été pressée
        /// </summary>
        /// <param name="key">Une touche du clavier</param>
        /// <returns>Retourne true si la touche a été pressée, false sinon</returns>
        public static bool KeyPressed(Keys key)
        {
            return (keyboardState.IsKeyDown(key) && lastKeyboardState.IsKeyUp(key));
        }

        /// <summary>
        /// Indique si une touche est enfoncée
        /// </summary>
        /// <param name="key">Une touche du clavier</param>
        /// <returns>Retourne true si la touche est enfoncée, false sinon</returns>
        public static bool KeyDown(Keys key)
        {
            return keyboardState.IsKeyDown(key);
        }

        #endregion
    }
}
