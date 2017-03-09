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
        public long PlayerID
        {
            get; set;
        }

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

        public void TransferData(PlayerDeathGame m)
        {
            PlayerID = m.PlayerID;
        }
    }
}
