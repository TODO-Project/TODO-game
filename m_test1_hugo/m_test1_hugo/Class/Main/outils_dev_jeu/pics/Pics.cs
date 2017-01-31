using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Main.outils_dev_jeu.pics
{
    public abstract class Pics:Sprite
    {
        public static List<Pics> PicList = new List<Pics>();

        public abstract string textureName
        {
            get;
            set;
        }
        
        public abstract bool takeMsg
        {
            get;
            set;
        }

        public Pics(Vector2 position)
        {
            this.Position = new Vector2(position.X, position.Y - 30);
        }
    }
}
