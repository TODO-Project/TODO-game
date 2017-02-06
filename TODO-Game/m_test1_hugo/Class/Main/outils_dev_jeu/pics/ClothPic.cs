using entrainementProjet1.Class.Main;
using m_test1_hugo.Class.clothes;
using m_test1_hugo.Class.Main.Menus.pages;
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
    class ClothPic:Pics
    {
        public Cloth _cloth;

        public override string textureName
        {
            get;
            set;
        }

        public override bool takeMsg
        {
            get;
            set;
        }

        public override Vector2 Position { get; set; }

        public ClothPic(Cloth cloth, Vector2 position)
            :base(position)
        {
            _cloth = cloth;
            GamePage.PicList.Add(this);
            Console.WriteLine(Position);
            LoadContent(Game1.Content);
        }

        public override void LoadContent(ContentManager content)
        {
            if (_cloth is Pant)
                texture = content.Load<Texture2D>("clothes/pants/" + _cloth.clothName );
            else if(_cloth is Boots)
                texture = content.Load<Texture2D>("clothes/boots/" + _cloth.clothName);
            else if(_cloth is Shirt)
                texture = content.Load<Texture2D>("clothes/shirts/" + _cloth.clothName);
        }

        public new void Draw(SpriteBatch spriteBatch)
        {  
            spriteBatch.Draw(texture, Position, null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 1.0f);
        }
    }
}
