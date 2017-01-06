using m_test1_hugo.Class.Main;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using Microsoft.Xna.Framework.Input;
using m_test1_hugo.Class.Main.InputSouris;

namespace m_test1_hugo.Class.Weapons
{
    public class Sniper : Weapon
    {

        // Je ne dis pas que c'est la vérité absolue mais quand tu utilises une Propriété (getter/setter)
        // garde toujours une variable privée qui contient ta valeur
        // car ce que tu faisais avant créait une boucle infini de get/set
        // NB : par convention les variables privées, je mets un _ pour les différencier

        // Virer le new et mettre override pour écraser le getter
        public override Vector2 Position
        {
            get { return Holder.Center; }
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("sniper");
        }

        public Sniper()
        {
            this.Name = "sniper";
            this.ReloadingTime = 50;  // millisecondes
            this.RearmingTime = 50; // millisecondes
            this.MagazineSize = 3;
            this.CurrentAmmo = MagazineSize;
            this.Damages = 100;
            this.Range = 2000;
            this.bulletSpeed = 0.04F;
            this.bulletSprite = "ClassicBullet";
        }
    }
}