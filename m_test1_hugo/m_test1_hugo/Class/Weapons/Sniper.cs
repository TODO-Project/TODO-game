using m_test1_hugo.Class.Main;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace m_test1_hugo.Class.Weapons
{
    class Sniper : Weapon
    {
        // Je ne dis pas que c'est la vérité absolue mais quand tu utilises une Propriété (getter/setter)
        // garde toujours une variable privée qui contient ta valeur
        // car ce que tu faisais avant créait une boucle infini de get/set
        // NB : par convention les variables privées, je mets un _ pour les différencier
        private Character _holder;


        public Character Holder
        {
            get { return this._holder; }
            set {
                    // On récupère le parent
                    this._holder = value;                  
                }
        }

        // Virer le new et mettre override pour écraser le getter
        public override Vector2 Position
        {
            get { return Holder.Center; }
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("sniper");
        }

        public Sniper(Character holder)
        {
            this.Name = "sniper";
            /* this.Height = height;
             this.Width = width;*/
            this.ReloadingTime = 5000;
            this.MagazineSize = 3;
            this.RearmingTime = 2000;
            this.CurrentAmmo = MagazineSize;
            this.Damages = 100;
            this.Holder = holder;
        }
    }
}
