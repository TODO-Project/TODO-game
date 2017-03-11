using m_test1_hugo.Class.Main.Menus.pages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Main.Menus
{
    /// <summary>
    /// Classe abstraite, etat de jeu.
    /// Un etat de jeu sert a changer de "page" pour eviter de relancer une nouvelle fenetre, et donc un autre "sous-programme".
    /// </summary>
    public class GameState
    {
        public MenuPage activePage;

        public GameState()
        {
            Console.WriteLine("gameState Created");
            activePage = new MainPage();
        }

        public void Draw(SpriteBatch spriteBatch)//okk juste draw les boutons
        {
            activePage.Update();
            activePage.Draw(spriteBatch);

            var action = activePage.Action();
            if (action != null)
            {
                activePage = action;
            }
        }
    }
}
