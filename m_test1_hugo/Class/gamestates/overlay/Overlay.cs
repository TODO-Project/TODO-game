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
    /// <summary>
    /// Overlay d'affichage de la santé, des munitions, etc....
    /// </summary>
    public class Overlay : Sprite
    {
        SpriteFont font; // on définit une police d'écriture pour les mesages liés à l'overlay

        #region messages
        string ammo, health, reloading = "RELOADING" ;
        public string takeWeapon = "Press E again to take this Weapon";
        string pressButton = "Press E to open the secret box !";
        #endregion
        private Player player; // joueur associé à l'overlay (client)
        
        #region positions - size
        Vector2 FooterPosition, ammoPosition, healthPosition;
        float BodyScale;
        #endregion

        Texture2D Body;
        Pics pic;

        public Overlay(Player player)
        {
            this.player = player;
            LoadContent(Game1.Content);
        }

        #region positions    
        Vector2 ReloadingPosition
        {
            get { return new Vector2(Game1.WindowWidth / 2, Game1.WindowHeight / 2); }
        }

        Vector2 weaponPicPosition
        {
            get { return new Vector2(Game1.WindowWidth / 2, Game1.WindowHeight / 2 - 50); }
        }

        Vector2 BodyPosition
        {
            get { return Vector2.Zero; }
        }

        #endregion

        public override void LoadContent(ContentManager content)
        {
            // on charge les textures
            texture = content.Load<Texture2D>("Overlay/footer");
            Body = content.Load<Texture2D>("Overlay/nude");
            font = content.Load<SpriteFont>("font");
            FooterPosition = new Vector2(Game1.WindowWidth / 2 - Width / 2, Game1.WindowHeight - Height);
            ammoPosition = new Vector2(FooterPosition.X + Width - Width/4, FooterPosition.Y + Height /3);
            healthPosition = new Vector2(FooterPosition.X + Width / 7, FooterPosition.Y + Height / 3);
            BodyScale = 1f;
        }
        
        public void Update(GameTime gametime)
        {
            // on récupère les stats joueur + balles dans l'arme
            if(player.weapon != null)
                ammo = player.weapon.CurrentAmmo + " / " + player.weapon.MagazineSize;
            health = player.Health + " / " + player.MaxHealth;
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            Update(Game1.gameTime);// on met a jour
            spriteBatch.Draw(texture, FooterPosition, Color.White);

            //on dessine toutes les infos
            if (GamePage.player.weapon != null)
            {
                if(ammo != null)
                    spriteBatch.DrawString(font, ammo, ammoPosition, Color.White, 0, Vector2.Zero, 0.26f, SpriteEffects.None, 1f);
            }
                
            spriteBatch.DrawString(font, health.ToString(), healthPosition, Color.White, 0, Vector2.Zero , 0.26f, SpriteEffects.None, 1f);

            if (GamePage.player.weapon != null)
            {
                if (GamePage.player.weapon.NeedReloading)
                    spriteBatch.DrawString(font, reloading, ReloadingPosition, Color.FloralWhite, 0, new Vector2(font.MeasureString(reloading).X / 2, font.MeasureString(reloading).Y), 0.5f, SpriteEffects.None, 1f);
            }

            spriteBatch.Draw(Body, BodyPosition, null, Color.White, 0f, Vector2.Zero, BodyScale, SpriteEffects.None, 1f);

            //on dessine le message si le personnage est sur une "weaponPic"
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

            //on dessine le message si le personnage est sur une "magicBox"
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
