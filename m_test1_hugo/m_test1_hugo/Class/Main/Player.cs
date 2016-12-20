using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
<<<<<<< HEAD
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace m_test1_hugo.Class.Main
{
    class Player : Character
    {
        
=======
using m_test1_hugo.Class.Weapons;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using m_test1_hugo.Class.Main.interfaces;

namespace m_test1_hugo.Class.Main
{
    class Player : Character, Movable
    {
        public Player(Character classe)
        {
            MoveSpeed = classe.MoveSpeed;
        }
>>>>>>> ajout Interface "Clickable" + pb texture loading

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("player");
        }
<<<<<<< HEAD
=======

        public void Update(GameTime gametime)
        {
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.A))
            {
                if (this.Position.X >= 0 + this.MoveSpeed)
                    this.Position = new Vector2(this.Position.X - this.MoveSpeed, this.Position.Y);

            }

            if (state.IsKeyDown(Keys.Down) || state.IsKeyDown(Keys.S))
            {

                if (this.Position.Y + this.Height <= Game1.WindowHeight - this.MoveSpeed)
                    this.Position = new Vector2(this.Position.X, this.Position.Y + this.MoveSpeed);

            }

            if (state.IsKeyDown(Keys.Up) || state.IsKeyDown(Keys.W))
            {
                if (this.Position.Y >= 0 + this.MoveSpeed)
                    this.Position = new Vector2(this.Position.X, this.Position.Y - this.MoveSpeed);
            }

            if (state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.D))
            {
                if (this.Position.X + this.Width <= Game1.WindowWidth - this.MoveSpeed)
                    this.Position = new Vector2(this.Position.X + this.MoveSpeed, this.Position.Y);
            }
        }
>>>>>>> ajout Interface "Clickable" + pb texture loading
    }
}
