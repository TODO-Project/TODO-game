using m_test1_hugo.Class.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Characters.Teams
{
    public class TeamBlue : Team
    {
        public static List<Character> TeamBlueList = new List<Character>();

        public new string name = "blue";

        public new int teamNumber = 1;

        public new int nbPlayers
        {
            get { return TeamBlueList.Count; }
        }
    }
}
