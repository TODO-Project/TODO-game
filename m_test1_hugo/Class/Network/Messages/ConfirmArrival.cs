using m_test1_hugo.Class.Network.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace m_test1_hugo.Class.Network.Messages
{
    /// <summary>
    /// Décrit un message de confirmation d'arrivée en jeu
    /// </summary>
    public class ConfirmArrival : ServerMessage
    {
        /// <summary>
        /// Crée un message de confirmation d'arrivée en jeu
        /// </summary>
        public ConfirmArrival()
        {
            MessageType = Types.ServerMessageTypes.ConfirmArrival;    
        }

        /// <summary>
        /// Décode un message de confirmation d'arrivée en jeu
        /// </summary>
        /// <param name="msg">Le message entrant</param>
        public override void DecodeMessage(NetIncomingMessage msg)
        {
            MessageType = (Types.ServerMessageTypes)msg.ReadByte();
        }

        /// <summary>
        /// Encode un message de confirmation d'arrivée en jeu
        /// </summary>
        /// <param name="msg">Le message sortant</param>
        public override void EncodeMessage(NetOutgoingMessage msg)
        {
            msg.Write((byte)MessageType);
        }
    }
}
