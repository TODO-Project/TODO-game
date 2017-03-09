using m_test1_hugo.Class.Main.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace m_test1_hugo.Class.Main.Menus
{
    public class Button : Sprite, Clickable
    {
        public bool clicked, rightClicked;
        public bool selected;

        private string text;
        public Object value;
        public string Text
        {
            get { return text; }
        }

        public SpriteFont font;
        private Vector2 textPosition;

        public Button(string text)
        {
            this.text = text;
            LoadContent(Game1.Content);
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("menu/button");
            font = content.Load<SpriteFont>("font");
        }

        public bool leftClick()
        {
            if (Bounds.Contains(Mouse.GetState().Position.X, Mouse.GetState().Position.Y) && Mouse.GetState().LeftButton == ButtonState.Pressed && !clicked)
            {
                clicked = true;
                return false;
            }
            else if (clicked && Bounds.Contains(Mouse.GetState().Position.X, Mouse.GetState().Position.Y))
            {
                if (Mouse.GetState().LeftButton == ButtonState.Released)
                    return true;
            }
            else
            {
                clicked = false;
            }
            return false;
        }

        public bool rightClick()
        {
            if (Bounds.Contains(Mouse.GetState().Position.X, Mouse.GetState().Position.Y) && Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                rightClicked = true;
                return false;
            }
            else if (rightClicked && Bounds.Contains(Mouse.GetState().Position.X, Mouse.GetState().Position.Y))
            {
                if (Mouse.GetState().RightButton == ButtonState.Released)
                    return true;
            }
            else
            {
                rightClicked = false;
            }
            return false;
        }

        public new void Draw (SpriteBatch spriteBatch)
        {
            LoadContent(Game1.Content);
            spriteBatch.Draw(texture, Position, Color.White);
            textPosition = this.Center;
            DrawText(spriteBatch);
        }

        public void DrawText(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, text, Center, Color.GhostWhite, 0f, new Vector2(font.MeasureString(text).X / 2, font.MeasureString(text).Y / 2), 0.23f, SpriteEffects.None, 1f);
        }

    }
}
