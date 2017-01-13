using m_test1_hugo.Class.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Characters.Teams
{
    public class Team
    {

        public static List<Character> TeamPlayerList = new List<Character>();

        public int _teamNumber;

        public string _name;

        public int _nbPlayers
        {
            get { return TeamPlayerList.Count(); }
        }

        public Team(int teamNumber, string name)
        {
            _teamNumber = teamNumber;
            _name = name;
        }

    }
}
