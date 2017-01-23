using entrainementProjet1.Class.Main;
using m_test1_hugo.Class.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.clothes
{
    public class Shirt : Cloth
    {
        int _speedBonus;
        int _healthBonus;
        string _name;

        public override Vector2 Position
        {
            get { return new Vector2(0, 0); }
        }

        public Shirt(string name, int speedBonus, int healthBonus)
        {
            this._speedBonus = speedBonus;
            this._healthBonus = healthBonus;
            this._name = name;
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("clothes/shirts/" + _name);
        }

        public override void interract(Player player)
        {
            player.ClothesList[0] = this;

            player.MaxHealth = player.classe.Health + _healthBonus;
            player.Health = player.MaxHealth;
        }
    }
}
