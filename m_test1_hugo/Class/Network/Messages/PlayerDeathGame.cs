using m_test1_hugo.Class.Network.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace m_test1_hugo.Class.Network.Messages
{
    public class PlayerDeathGame : GameMessage
    {
        /// <summary>
        /// L'ID du joueur
        /// </summary>
        public long PlayerID
        {
            get; set;
        }

        /// <summary>
        /// Crée un message de mort vide
        /// </summary>
        public PlayerDeathGame()
        {
            MessageType = Types.GameMessageTypes.SendDeath;
        }

        /// <summary>
        /// Crée un message de mort selon l'ID du joueur
        /// </summary>
        /// <param name="ID">L'ID du joueur</param>
        public PlayerDeathGame(long ID)
            : this ()
        {
            PlayerID = ID;
        }

        public override void DecodeMessage(NetIncomingMessage msg)
        {
            PlayerID = msg.ReadInt64();
        }

        public override void EncodeMessage(NetOutgoingMessage msg)
        {
            msg.Write((byte)MessageType);
            msg.Write(PlayerID);
        }
    }
}
