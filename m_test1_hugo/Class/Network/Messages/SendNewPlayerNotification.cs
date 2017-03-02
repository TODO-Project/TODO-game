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
    /// <summary>
    /// Décrit un message d'arrivée de nouveau joueur, notifiée quand un nouveau joueur entre en jeu
    /// </summary>
    class SendNewPlayerNotification : ServerMessage
    {
        /// <summary>
        /// Récupère et définit le pseudo du nouveau joueur
        /// </summary>
        public string Pseudo
        {
            get; set;
        }

        /// <summary>
        /// Récupère et définit l'ID unique du nouveau joueur
        /// </summary>
        public int PlayerID
        {
            get; set;
        }

        /// <summary>
        /// Construit un message d'arrivée de nouveau joueur
        /// </summary>
        public SendNewPlayerNotification()
        {
            MessageType = ServerMessageTypes.SendNewPlayerNotification;
        }

        /// <summary>
        /// Construit un message d'arrivée de nouveau joueur en précisant le pseudo
        /// </summary>
        /// <param name="pseudo">Le pseudo du nouveau joueur</param>
        public SendNewPlayerNotification(string pseudo, NetConnection connection)
            : this()
        {
            Pseudo = pseudo;
            PlayerID = connection.GetHashCode();
        }

        /// <summary>
        /// Décode un message entrant de venue de nouveau joueur
        /// </summary>
        /// <param name="msg">Un message entrant</param>
        public override void DecodeMessage(NetIncomingMessage msg)
        {
            //MessageType = (ServerMessageTypes)msg.ReadByte();
            PlayerID = msg.ReadInt32();
            Pseudo = msg.ReadString();
        }

        /// <summary>
        /// Encode un message sortant de venue de nouveau joueur
        /// </summary>
        /// <param name="msg">Un message sortant</param>
        public override void EncodeMessage(NetOutgoingMessage msg)
        {
            msg.Write((byte)MessageType);
            msg.Write(PlayerID);
            msg.Write(Pseudo);
        }
    }
}
