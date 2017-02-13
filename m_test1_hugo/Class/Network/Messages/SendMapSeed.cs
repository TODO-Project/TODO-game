using m_test1_hugo.Class.Network.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;
using m_test1_hugo.Class.Network.Messages.Types;

namespace m_test1_hugo.Class.Network.Messages
{
    public class SendMapSeed : ServerMessage
    {
        public int GameSeed
        {
            get; set;
        }

        public SendMapSeed(int gameSeed)
        {

        }

        public override void DecodeMessage(NetIncomingMessage msg)
        {
            MessageType = (ServerMessageTypes)msg.ReadByte();
            GameSeed = msg.ReadInt32();

        }

        public override void EncodeMessage(NetOutgoingMessage msg)
        {
            msg.Write((byte)MessageType);
            msg.Write(GameSeed);
        }
    }
}
