using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Network.Messages.Types
{
    /// <summary>
    /// Énumère tous les types de messages clients
    /// </summary>
    public enum GameMessageTypes
    {
        GetMapSeed = 1,      
        SendPlayerData = 2,
        SendArrival = 3,
        NewBulletGame = 4,
        SendPlayerRespawn = 5
    }
}
