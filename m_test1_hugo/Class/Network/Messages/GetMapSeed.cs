using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;
using m_test1_hugo.Class.Network.Messages.Types;
using m_test1_hugo.Class.Network.Abstract;

namespace m_test1_hugo.Class.Network.Messages
{
    /// <summary>
    /// Décrit le message de demande de graine
    /// </summary>
    public class GetMapSeed : GameMessage
    {
        /// <summary>
        /// Construit le message
        /// </summary>
        public GetMapSeed()
        {
            MessageType = GameMessageTypes.GetMapSeed;
        }

        /// <summary>
        /// Décode un message de demande de graine
        /// </summary>
        /// <param name="msg">Le message entrant</param>
        public override void DecodeMessage(NetIncomingMessage msg)
        {
            MessageType = (GameMessageTypes)msg.ReadByte();
        }

        /// <summary>
        /// Encode un message de demande de graine
        /// </summary>
        /// <param name="msg">Le message sortant</param>
        public override void EncodeMessage(NetOutgoingMessage msg)
        {
            msg.Write((byte)MessageType);
        }
    }
}
