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
        GetMapSeed = 1,          // Message de demande de récupération de seed
        SendPlayerData = 2,      // Envoi des données du joueur
        SendArrival = 3,         // Message d'arrivée du joueur sur le serveur
        NewBulletGame = 4,       // Envoi d'un message de nouvelle balle
        SendPlayerRespawn = 5,   // Message de respawn du joueur
        SendDeath = 6,           // Message de mort du joueur
        SendDisconnection = 7    // Message de déconnexion du joueur
    }
}
