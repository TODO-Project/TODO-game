using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using m_test1_hugo.Class.Main;

namespace m_test1_hugo.Class.Tiles
{
    public class GrassTile : Sprite
    {
        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("grass");
        }
    }
}
