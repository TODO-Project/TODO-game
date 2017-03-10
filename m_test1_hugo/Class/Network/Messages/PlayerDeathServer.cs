using m_test1_hugo.Class.Network.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace m_test1_hugo.Class.Network.Messages
{
    class PlayerDeathServer : ServerMessage
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
        public PlayerDeathServer()
        {
            MessageType = Types.ServerMessageTypes.Death;
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

        /// <summary>
        /// Transfère les données du message entrant
        /// </summary>
        /// <param name="m">Le message entrant</param>
        public void TransferData(PlayerDeathGame m)
        {
            PlayerID = m.PlayerID;
        }
    }
}
