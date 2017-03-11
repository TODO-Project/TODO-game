using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
    /// Une page de menu, pouvant contenir des boutons et de champs de texte
    /// </summary>
    public abstract class MenuPage
    {
        public List<Button> buttons = new List<Button>();

        public abstract MenuPage Action();
        public abstract void Update();
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}

