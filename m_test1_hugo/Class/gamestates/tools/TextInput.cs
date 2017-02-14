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
    public class TextInput:Button
    {
        #region attributs
        private bool released;
        private Keys lastKeyPressed;
        private SpriteFont font;
        private bool focus;
        private Color color;
        private int length;
        private string value;

        #endregion

        #region prop
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
                return new Vector2(Position.X + 40, Position.Y + 210);
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
        /*public string Text
        {
            get
            {
                return text;
            }

            private set
            {
                text = value;
            }
        }*/

        public bool Focus
        {
            get
            {
                return focus;
            }

            set
            {
                focus = value;
            }
        }

        public Color Color
        {
            get
            {
                return color;
            }

            set
            {
                color = value;
            }
        }

        public string Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;
            }
        }
        #endregion

        #region construct
        public TextInput(string label, int length)
            :base(label)
        {
            Label = label;
            Value = "";
            Length = length;
            released = true;
        }
        #endregion

        #region methodes
        public void Update()
        {
            #region focus
            if (leftClick())
            {
                Focus = true;
            }
            if (Focus && Mouse.GetState().LeftButton == ButtonState.Pressed && !this.Bounds.Contains(Mouse.GetState().Position))
                Focus = false;
            #region color
            if (Focus)
                color = Color.Firebrick;
            else
                color = Color.White;
            #endregion
            #endregion
            #region input
            if (Focus)
            {
                KeyboardState kb = Keyboard.GetState();
                if (kb.GetPressedKeys().Length > 0 && Value.Length < Length) // si une touche est pressed et la taille de la string est < 16
                {
                    var lastIndex = kb.GetPressedKeys().Length - 1; // on recupere l'index de la derniere touche appuyee
                    var key = kb.GetPressedKeys()[lastIndex]; // on recupere la derniere touche appuyee
                    if (key != Keys.Back && (key != lastKeyPressed || released)) // si on appuie sur une autre toushe que backspace
                    {
                        string s = key.ToString().ToLower();

                        if (s.Substring(0, s.Length - 1) == "numpad")
                        {
                            s = s.Substring(s.Length - 1, 1);
                        }
                        else if (s == "decimal")
                            s = ".";
                        Console.WriteLine(s);
                        Regex r = new Regex("^[a-zA-Z0-9\\.]{1}$");  // expression reguliere pour tester si alphanumerique
                        if (r.IsMatch(s)) // si la touche appuyee est alphanum
                        {
                            value += s; // on ajoute la lettre associee a la touche
                            released = false;
                        }
                        lastKeyPressed = key;
                    }
                }
                else
                    released = true;
                if (released && kb.IsKeyDown(Keys.Back)) // si on appuie sur backspace
                {
                    if (value.Length >= 1) // si on a quelquechose a effacer
                        value = value.Substring(0, value.Length - 1); // on efface
                    released = false;
                }
            }
            #endregion
        }

        public override void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("font");
            texture = content.Load<Texture2D>("menu/textarea");
            color = Color.White;
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            LoadContent(Game1.Content);
            Update();
            spriteBatch.Draw(texture, Position, color);
            spriteBatch.DrawString(font, Label, LabelPosition, Color.White, 0f, Vector2.Zero, 0.35f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(font, value, TextPosition, Color.White, 0f, Vector2.Zero, 0.25f, SpriteEffects.None, 1f);
        }
        #endregion
    }
}
