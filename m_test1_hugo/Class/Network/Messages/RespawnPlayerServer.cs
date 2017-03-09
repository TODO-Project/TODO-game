﻿using m_test1_hugo.Class.Network.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace m_test1_hugo.Class.Network.Messages
{
    public class RespawnPlayerServer : ServerMessage
    {
        /// <summary>
        /// L'ID du joueur
        /// </summary>
        public long ID
        {
            get; set;
        }

        public RespawnPlayerServer()
        {
            MessageType = Types.ServerMessageTypes.PlayerRespawn;
        }

        public override void DecodeMessage(NetIncomingMessage msg)
        {
            ID = msg.ReadInt64();
        }

        public override void EncodeMessage(NetOutgoingMessage msg)
        {
            msg.Write((byte)MessageType);
            msg.Write(ID);
        }

        public void TransferData(RespawnPlayerGame m)
        {
            ID = m.ID;
        }
    }
}
