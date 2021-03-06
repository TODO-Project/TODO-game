﻿using m_test1_hugo.Class.Network.Abstract;
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
        /// <summary>
        /// L'ID du joueur
        /// </summary>
        public long PlayerID
        {
            get; set;
        }

        /// <summary>
        /// L'angle de tir de la balle
        /// </summary>
        public float AngleTir
        {
            get; set;
        }

        /// <summary>
        /// Crée un message de nouvelle balle
        /// </summary>
        public NewBulletServer()
        {
            MessageType = ServerMessageTypes.NewBulletServer;
        }

        public override void DecodeMessage(NetIncomingMessage msg)
        {
            MessageType = (ServerMessageTypes)msg.ReadByte();
            PlayerID = msg.ReadInt64();
            AngleTir = msg.ReadFloat();
        }

        public override void EncodeMessage(NetOutgoingMessage msg)
        {
            msg.Write((byte)MessageType);
            msg.Write(PlayerID);
            msg.Write(AngleTir);
        }

        /// <summary>
        /// Transfère les données d'un message client vers ce message
        /// </summary>
        /// <param name="msg">Le message du client</param>
        public void TransferData(NewBulletGame msg)
        {
            PlayerID = msg.PlayerID;
            AngleTir = msg.AngleTir;
        }
    }
}
