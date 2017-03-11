using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Main.Menus.healthBar
{
    /// <summary>
    /// Barre de sante des joueurs
    /// </summary>
    public class HealthBar:Sprite
    {
        //width : 66, height : 6
        public Texture2D lifeRectangle;
        int lifeRectangleWidth;
        public Player Holder;
        Color color;

        public object ScreenManager { get; private set; }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("players/healthBar");
            lifeRectangle = content.Load<Texture2D>("players/green");
        }

        public void Update(GameTime gameTime)
        {
            Position = new Vector2(Holder.Position.X-Holder.Width/4, Holder.Position.Y-20);
            lifeRectangleWidth = Holder.Health * 66 / Holder.MaxHealth;
            if (lifeRectangleWidth <= 33)
                color = Color.Orange;
            if (lifeRectangleWidth < 18)
                color = Color.Red;
            if(lifeRectangleWidth > 33)
                color = Color.Green;
        }

        public HealthBar(Player Holder)
        {
            this.Holder = Holder;
            LoadContent(Game1.Content);
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            Update(Game1.gameTime);
            spriteBatch.Draw(texture, Position, Color.White);
            spriteBatch.Draw(lifeRectangle, new Rectangle((int)Position.X+1, (int)Position.Y+1, lifeRectangleWidth, 6), color);
        }

    }
}
