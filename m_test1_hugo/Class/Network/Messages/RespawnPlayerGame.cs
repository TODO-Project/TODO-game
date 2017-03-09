﻿using m_test1_hugo.Class.Network.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace m_test1_hugo.Class.Network.Messages
{
    public class RespawnPlayerGame : GameMessage
    {
        /// <summary>
        /// L'ID du joueur à respawn
        /// </summary>
        public long ID
        {
            get; set;
        }

        public RespawnPlayerGame()
        {
            MessageType = Types.GameMessageTypes.SendPlayerRespawn;
        }

        public RespawnPlayerGame(long id)
            : this ()
        {
            ID = id;
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
    }
}