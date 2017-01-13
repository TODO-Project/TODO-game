using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace m_test1_hugo.Class.Main.overlay
{
    public class Overlay
    {
        
        SpriteFont font;
        string ammo, health, reloading = "RELOADING" ;

        #region positions
        Vector2 ammoPosition
        {
            get { return new Vector2(Game1.WindowWidth - Game1.WindowWidth/20, Game1.WindowHeight - Game1.WindowWidth/40); }
        }
        Vector2 healthPosition
        {
            get { return new Vector2( Game1.WindowWidth / 10,  Game1.WindowHeight - Game1.WindowWidth / 40); }
        }

        #endregion

        public Rectangle footer = new Rectangle((int)Vector2.Zero.X, Game1.WindowHeight - Game1.WindowWidth / 40 - 50, Game1.WindowWidth, Game1.WindowWidth / 40 + 50);



        #region recuperation des munitions de l'utilisateur;
        public int maxAmmo
        {
            get { return Game1.player.weapon.MagazineSize; }
        }

        private int _currentAmmo;
        public int currentAmmo
        {
            get { return _currentAmmo; }
            set { _currentAmmo = value; }
        }

        #endregion

        public void LoadContent(ContentManager content)
        {
            //texture = content.Load<Texture2D>("overlay");
            font = content.Load<SpriteFont>("font");

        }
        
        public void Update(GameTime gametime)
        {
            ammo = Game1.player.weapon.CurrentAmmo + " / " + Game1.player.weapon.MagazineSize;
            health = Game1.player.Health + " / " + Game1.player.MaxHealth;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, ammo, ammoPosition, Color.Turquoise, 0, font.MeasureString(ammo) , 4.0f, SpriteEffects.None, 0.2f);
            spriteBatch.DrawString(font, health, healthPosition, Color.Turquoise, 0, new Vector2(0, font.MeasureString(health).Y) , 4.0f, SpriteEffects.None, 0.2f);
            if(Game1.player.weapon.NeedReloading)
                spriteBatch.DrawString(font, reloading, new Vector2(Game1.WindowWidth/2, Game1.WindowHeight/2), Color.White, 0, new Vector2(font.MeasureString(reloading).X/2, font.MeasureString(reloading).Y), 4.0f, SpriteEffects.None, 0.2f);
        }

    }
}
