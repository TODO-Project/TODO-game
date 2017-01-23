using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using m_test1_hugo.Class.Main.outils_dev_jeu.ArmesVignette;
using m_test1_hugo.Class.Bonuses;

namespace m_test1_hugo.Class.Main.overlay
{
    public class Overlay : Sprite
    {
        
        SpriteFont font;
        string ammo, health, reloading = "RELOADING" ;
        public string takeWeapon = "Press E again to take this Weapon";
        string pressButton = "Press E to open the secret box !";

        Texture2D Body;

        #region positions
        Vector2 ammoPosition
        {
            get { return new Vector2(Game1.WindowWidth - Game1.WindowWidth/4, Game1.WindowHeight - Height/ 1.7f); }
        }
        Vector2 healthPosition
        {
            get { return new Vector2( Game1.WindowWidth / 10,  Game1.WindowHeight - Height/ 1.7f); }
        }

        Vector2 ReloadingPosition
        {
            get { return new Vector2(Game1.WindowWidth / 2, Game1.WindowHeight / 2); }
        }

        Vector2 weaponPicPosition
        {
            get { return new Vector2(Game1.WindowWidth / 2, Game1.WindowHeight / 2 - 50); }
        }

        Vector2 FooterPosition
        {
            get { return new Vector2(0, Game1.WindowHeight - Height/1.5f); }
        }

        Vector2 BodyPosition
        {
            get { return Vector2.Zero; }
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

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Overlay/footer");
            Body = content.Load<Texture2D>("Overlay/nude");
            font = content.Load<SpriteFont>("font");
        }
        
        public void Update(GameTime gametime)
        {
            ammo = Game1.player.weapon.CurrentAmmo + " / " + Game1.player.weapon.MagazineSize;
            health = Game1.player.Health + " / " + Game1.player.MaxHealth;
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, FooterPosition, footer, Color.White);
            spriteBatch.DrawString(font, ammo, ammoPosition, Color.DarkRed, 0, Vector2.Zero, 4.0f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(font, health, healthPosition, Color.DarkRed, 0, Vector2.Zero , 4.0f, SpriteEffects.None, 1f);

            if (Game1.player.weapon.NeedReloading)
                spriteBatch.DrawString(font, reloading, ReloadingPosition, Color.FloralWhite, 0, new Vector2(font.MeasureString(reloading).X/2, font.MeasureString(reloading).Y), 4.0f, SpriteEffects.None, 0.2f);

            spriteBatch.Draw(Body, BodyPosition, null, Color.White);

            #region weaponPics
            for (var i = 0; i < WeaponPic.WeaponPicList.Count; i++)
            {
                WeaponPic weaponPic = WeaponPic.WeaponPicList[i];
                if (weaponPic.takeWeaponMsg)
                {
                    spriteBatch.DrawString(font, takeWeapon, weaponPicPosition, Color.DarkTurquoise, 0, new Vector2(font.MeasureString(takeWeapon).X / 2, font.MeasureString(takeWeapon).Y), 2.0f, SpriteEffects.None, 0.2f);
                }
            }
            #endregion

            #region magicBox
            for (var i = 0; i < Bonus.BonusList.Count; i++)
            {
                Bonus bonus = Bonus.BonusList[i];

                if(bonus.name == "magicBox")
                {
                    MagicBox magicBox = (MagicBox)bonus;
                    if (magicBox.pressButtonMsg)
                    {
                        spriteBatch.DrawString(font, pressButton, weaponPicPosition, Color.DarkTurquoise, 0, new Vector2(font.MeasureString(takeWeapon).X / 2, font.MeasureString(takeWeapon).Y), 2.0f, SpriteEffects.None, 0.2f);
                    }
                }  
            }
            #endregion
        }
    }
}
