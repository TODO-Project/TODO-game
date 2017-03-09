using System;
using m_test1_hugo.Class.Weapons;
using m_test1_hugo.Class.Main;
using Microsoft.Xna.Framework;
using m_test1_hugo.Class.Main.Menus;

namespace m_test1_hugo
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            using (var game1 = new Game1())
            {
                game1.Run();
            }
        }
    }
#endif
}
