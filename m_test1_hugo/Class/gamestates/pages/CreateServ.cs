﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using m_test1_hugo.Class.Main.Menus.tools;
using Microsoft.Xna.Framework.Input;
using m_test1_hugo.Class.gamestates.tools;
using m_test1_hugo.Class.Characters.Teams;
using m_test1_hugo.Class.Weapons;

namespace m_test1_hugo.Class.Main.Menus.pages
{
    class CreateServ:MenuPage
    {
        private Texture2D bgTexture = Game1.Content.Load<Texture2D>("bg");
        private Vector2 bgPosition = Vector2.Zero;
        KeyboardState kb;
        private TextInput PseudoInput;
        private WeaponPicker wpPicker;
        //private TeamPicker tPicker;
        private Vector2 wpPickerPosition;

        public CreateServ()
        {
            buttons.Add(new Button("Create Game"));
            buttons.Add(new Button("Join Game"));
            buttons.Add(new Button("Back"));
            PseudoInput = new TextInput("Ton pseudo :", 16);
            PseudoInput.Position = new Vector2(80, Game1.WindowHeight/2-50);
            wpPickerPosition = new Vector2(800, Game1.WindowHeight / 2 - 50);
            wpPicker = new WeaponPicker(wpPickerPosition);
            /*tPicker = new TeamPicker(new Vector2(775, Game1.WindowHeight / 2 - 90));
            tPicker.Position = new Vector2(Game1.WindowWidth/2+150, Game1.WindowHeight/2 -50);*/
        }


        public override void Update()
        {

        }

        public override MenuPage Action()
        {
            if (buttons[0].leftClick() || kb.IsKeyDown(Keys.Enter))
            {
                GamePage.server = new Network.Server();
                GamePage.server.Start();
                GamePage.client = new Network.Client("10.103.253.1", 12345);
                GamePage.client.Start();
                return new GamePage(Weapon.List[wpPicker.WeaponCount], PseudoInput.Text, wpPicker.ActiveTeam);
            }
            if (buttons[1].leftClick())
            {
                GamePage.client = new Network.Client("10.103.253.1", 12345);
                GamePage.client.Start();
                return new GamePage(Weapon.List[wpPicker.WeaponCount], PseudoInput.Text, wpPicker.ActiveTeam);
            }
	    /*if (buttons[2].leftClick())
	    {
		    return new MultiPage();
        }*/
            else
                return null;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bgTexture, bgPosition, Color.White);
            kb = Keyboard.GetState();
            var currentY = 50;
            var currentX = 0;

            for (var i = 0; i < Bullet.BulletList.Count; i++)
            {
                Bullet currentBullet = (Bullet)Bullet.BulletList[i];
                currentBullet.Update(Game1.gameTime, 32, Game1.WindowWidth, Game1.WindowHeight);
                currentBullet.Draw(spriteBatch);
            }

            foreach (Button button in buttons)
            {
                button.Position = new Vector2(currentX, currentY);
                currentY += 130;
                button.Draw(spriteBatch);
            }
            PseudoInput.Draw(spriteBatch);
            //tPicker.Update();
            wpPicker.Update();
        }
    }
}