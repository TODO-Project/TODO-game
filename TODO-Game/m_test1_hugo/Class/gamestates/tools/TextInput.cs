using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Text.RegularExpressions;

namespace m_test1_hugo.Class.Main.Menus.tools
{
    public class TextInput:Sprite
    {
        #region attributs
        private bool released;
        private Keys lastKeyPressed;
        private SpriteFont font;

        private int length;
        public int Length
        {
            get
            {
                return length;
            }

            private set
            {
                length = value;
            }
        }

        private Vector2 TextPosition
        {
            get
            {
                return new Vector2(Position.X + 40,Position.Y + 215);
            }
        }
        private Vector2 LabelPosition
        {
            get
            {
                return new Vector2(Position.X + 30, Position.Y + 30);
            }
        }

        private string label;
        public string Label
        {
            get
            {
                return label;
            }

            private set
            {
                label = value;
            }
        }



        private string text;
        public string Text
        {
            get
            {
                return text;
            }

            private set
            {
                text = value;
            }
        }
        #endregion

        #region construct
        public TextInput(string label, int length)
        {
            Label = label;
            text = "";
            Length = length;
            released = true;
        }
        #endregion

        #region methodes
        public void Update()
        {
            KeyboardState kb = Keyboard.GetState();
            if (kb.GetPressedKeys().Length > 0 && text.Length < Length) // si une touche est pressed et la taille de la string est < 16
            {
                var lastIndex = kb.GetPressedKeys().Length - 1; // on recupere l'index de la derniere touche appuyee
                var key = kb.GetPressedKeys()[lastIndex]; // on recupere la derniere touche appuyee
                if (key != Keys.Back && (key != lastKeyPressed || released)) // si on appuie sur une autre toushe que backspace
                {
                    Regex r = new Regex("^[a-zA-Z0-9]{1}$");  // expression reguliere pour tester si alphanumerique
                    if (r.IsMatch(key.ToString())) // si la touche appuyee est alphanum
                    {
                        text += key; // on ajoute la lettre associee a la touche
                        released = false;
                    }
                    lastKeyPressed = key;
                }
            }
            else
                released = true;
            if (Keyboard.GetState().IsKeyDown(Keys.Back)) // si on appuie sur backspace
            {
                if (text.Length >= 1) // si on a quelquechose a effacer
                    text = text.Substring(0, text.Length - 1); // on efface
            }
        }

        public override void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("font");
            texture = content.Load<Texture2D>("menu/textarea");
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            LoadContent(Game1.Content);
            Update();
            spriteBatch.Draw(texture, Position, Color.White);
            spriteBatch.DrawString(font, Label, LabelPosition, Color.White, 0f, Vector2.Zero, 0.35f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(font, text, TextPosition, Color.White, 0f, Vector2.Zero, 0.25f, SpriteEffects.None, 1f);
        }
        #endregion
    }
}
