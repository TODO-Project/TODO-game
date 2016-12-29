using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrainementProjet1.Class.Main
{
    abstract class Cloth
    {
        //private Sprite sprite;

        private int magicalProtection;
        public int MagicalProtection
        {
            get
            {
                return magicalProtection;
            }

            set
            {
                magicalProtection = value;
            }
        }

        private int physicalProtection;
        public int PhysicalProtection
        {
            get
            {
                return physicalProtection;
            }

            set
            {
                physicalProtection = value;
            }
        }

         
    }
}
