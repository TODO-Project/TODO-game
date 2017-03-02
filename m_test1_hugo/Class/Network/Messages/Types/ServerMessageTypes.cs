using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Network.Messages.Types
{
    /// <summary>
    /// Énumère tous les types de messages serveur
    /// </summary>
    public enum ServerMessageTypes
    {
        SendMapSeed = 255,
        SendPlayerData = 254,
        SendNewPlayerNotification = 253,
        ConfirmArrival = 252
    }
}
