using m_test1_hugo.Class.Main;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Characters.Teams
{
    /// <summary>
    /// Une equipe que les joueurs integreront
    /// </summary>
    public class Team
    {
        public List<Player> TeamPlayerList = new List<Player>();
        public static List<Team> TeamList = new List<Team>();

        public int _teamNumber;

        public Color _Color
        {
            get;
            set;
        }

        public string _name;

        public int _nbPlayers
        {
            get { return TeamPlayerList.Count(); }
        }

        private int teamKills;
        public int TeamKills
        {
            get
            {
                return teamKills;
            }

            set
            {
                teamKills = value;
            }
        }

        private bool gameWon
        {
            get
            {
                return TeamKills >= 10;
            }
        }

        public Team(int teamNumber, string name, Color color)
        {   
            this._Color = color;
            _teamNumber = teamNumber;
            _name = name;
            Team.TeamList.Add(this);
        }

        public void Update()
        {
            if (gameWon)
                Console.WriteLine("team " + _name + " wins !");
        }

    }
}
