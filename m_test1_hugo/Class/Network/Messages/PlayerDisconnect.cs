using m_test1_hugo.Class.Network.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace m_test1_hugo.Class.Network.Messages
{
    public class PlayerDisconnect : ServerMessage
    {
        /// <summary>
        /// L'ID du joueur
        /// </summary>
        public long ID
        {
            get; set;
        }

        /// <summary>
        /// Crée un message de déconnexion du joueur vide
        /// </summary>
        public PlayerDisconnect()
        {
            MessageType = Types.ServerMessageTypes.Disconnection;
        }

        /// <summary>
        /// Crée un message de déconnexion du joueur selon l'ID du joueur
        /// </summary>
        /// <param name="id"></param>
        public PlayerDisconnect(long id)
            : this()
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
