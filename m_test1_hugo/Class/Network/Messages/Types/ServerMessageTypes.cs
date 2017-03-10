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
        SendMapSeed = 255,                  // Envoi de la seed de la map
        SendPlayerData = 254,               // Redistribution des données du joueur
        SendNewPlayerNotification = 253,    // Distribution du nouveau joueur
        ConfirmArrival = 252,               // Confirmation d'arrivée sur le serveur
        NewBulletServer = 251,              // Distribution d'une nouvelle balle aux joueurs
        PlayerRespawn = 250,                // Distribution du respawn d'un joueur
        Death = 249,                        // Distribution de la mort d'un joueur
        Disconnection = 248                 // Distribution de la déconnexion du joueur
    }
}
