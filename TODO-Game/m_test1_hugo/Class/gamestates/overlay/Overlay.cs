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
using m_test1_hugo.Class.Main.outils_dev_jeu.pics;
using m_test1_hugo.Class.Main.Menus.pages;

namespace m_test1_hugo.Class.Main.overlay
{
    public class Overlay : Sprite
    {
        
        SpriteFont font;
        string ammo, health, reloading = "RELOADING" ;
        public string takeWeapon = "Press E again to take this Weapon";
        string pressButton = "Press E to open the secret box !";
        private Player player;

        Texture2D Body;
        Pics pic;

        public Overlay(Player player)
        {
            this.player = player;
            LoadContent(Game1.Content);
        }

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

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Overlay/footer");
            Body = content.Load<Texture2D>("Overlay/nude");
            font = content.Load<SpriteFont>("font");
        }
        
        public void Update(GameTime gametime)
        {
            if(player.weapon != null)
                ammo = player.weapon.CurrentAmmo + " / " + player.weapon.MagazineSize;
            health = player.Health + " / " + player.MaxHealth;
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            Update(Game1.gameTime);
            spriteBatch.Draw(texture, FooterPosition, footer, Color.White);
            if (GamePage.player.weapon != null)
            {
                if(ammo != null)
                    spriteBatch.DrawString(font, ammo, ammoPosition, player.team._Color, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 1f);
            }
                
            spriteBatch.DrawString(font, health.ToString(), healthPosition, player.team._Color, 0, Vector2.Zero , 0.5f, SpriteEffects.None, 1f);

            if (GamePage.player.weapon != null)
            {
                if (GamePage.player.weapon.NeedReloading)
                    spriteBatch.DrawString(font, reloading, ReloadingPosition, Color.FloralWhite, 0, new Vector2(font.MeasureString(reloading).X / 2, font.MeasureString(reloading).Y), 0.5f, SpriteEffects.None, 1f);
            }

            spriteBatch.Draw(Body, BodyPosition, null, Color.White);

            #region weaponPics
            for (var i = 0; i < GamePage.PicList.Count; i++)
            {
                if(GamePage.PicList[i] is WeaponPic)
                    pic = (WeaponPic)GamePage.PicList[i];

                else if(GamePage.PicList[i] is ClothPic)
                    pic = (ClothPic)GamePage.PicList[i];

                if (pic.takeMsg)
                {
                    spriteBatch.DrawString(font, takeWeapon, weaponPicPosition, Color.Wheat, 0, new Vector2(font.MeasureString(takeWeapon).X / 2, font.MeasureString(takeWeapon).Y), 0.5f, SpriteEffects.None, 0.2f);
                }
            }
            #endregion

            #region magicBox
            for (var i = 0; i < GamePage.BonusList.Count; i++)
            {
                Bonus bonus = GamePage.BonusList[i];

                if(bonus is MagicBox)
                {
                    MagicBox magicBox = (MagicBox)bonus;
                    if (magicBox.pressButtonMsg)
                    {
                        spriteBatch.DrawString(font, pressButton, weaponPicPosition, Color.Wheat, 0, new Vector2(font.MeasureString(takeWeapon).X / 2, font.MeasureString(takeWeapon).Y), 0.5f, SpriteEffects.None, 0.2f);
                    }
                }
                else if(bonus is ClothBox)
                {
                    ClothBox clothBox = (ClothBox)bonus;
                    if (clothBox.pressButtonMsg)
                    {
                        spriteBatch.DrawString(font, pressButton, weaponPicPosition, Color.Wheat, 0, new Vector2(font.MeasureString(takeWeapon).X / 2, font.MeasureString(takeWeapon).Y), 0.5f, SpriteEffects.None, 0.2f);
                    }
                }  
            }
            #endregion

        }
    }
}
