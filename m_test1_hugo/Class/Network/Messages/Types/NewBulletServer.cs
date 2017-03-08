using m_test1_hugo.Class.Network.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace m_test1_hugo.Class.Network.Messages.Types
{
    public class NewBulletServer : ServerMessage
    {
        public long PlayerID
        {
            get; set;
        }

        public NewBulletServer()
        {
            MessageType = ServerMessageTypes.NewBulletServer;
        }

        public override void DecodeMessage(NetIncomingMessage msg)
        {
            MessageType = (ServerMessageTypes)msg.ReadByte();
            PlayerID = msg.ReadInt64();
        }

        public override void EncodeMessage(NetOutgoingMessage msg)
        {
            msg.Write((byte)MessageType);
            msg.Write(PlayerID);
        }

        public void TransferData(NewBulletGame msg)
        {
            PlayerID = msg.PlayerID;
        }
    }
}
