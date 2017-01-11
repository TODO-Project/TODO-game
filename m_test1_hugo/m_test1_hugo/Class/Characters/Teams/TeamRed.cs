using m_test1_hugo.Class.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Characters.Teams
{
    public class TeamRed : Team
    {
        public static List<Character> TeamRedList = new List<Character>();

        public new string name;

        public new int teamNumber;

        public new int nbPlayers
        {
            get { return TeamRedList.Count; }
        }
        public TeamRed()
        {
            teamNumber = 2;
        }
    }
}
