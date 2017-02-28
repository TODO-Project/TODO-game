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
    public class GetMapSeed : GameMessage
    {
        public GetMapSeed()
        {
            MessageType = GameMessageTypes.GetMapSeed;
        }

        public override void DecodeMessage(NetIncomingMessage msg)
        {
            MessageType = (GameMessageTypes)msg.ReadByte();
        }

        public override void EncodeMessage(NetOutgoingMessage msg)
        {
            msg.Write((byte)MessageType);
        }
    }
}
