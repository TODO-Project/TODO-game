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
    /// Décrit un message d'envoi de graine de génération vers le client.
    /// </summary>
    public class SendMapSeed : ServerMessage
    {
        /// <summary>
        /// Récupère et définit la graine de génération de la carte
        /// </summary>
        public int GameSeed
        {
            get; set;
        }

        /// <summary>
        /// Construit un message d'envoi de graine de génération selon la graine
        /// </summary>
        /// <param name="gameSeed">La graine de génération</param>
        public SendMapSeed(int gameSeed)
        {
            MessageType = ServerMessageTypes.SendMapSeed;
            GameSeed = gameSeed;
        }

        /// <summary>
        /// Décode un message d'envoi de graine aléatoire et remplit ses données internes
        /// </summary>
        /// <param name="msg">Le message entrant</param>
        public override void DecodeMessage(NetIncomingMessage msg)
        {
            MessageType = (ServerMessageTypes)msg.ReadByte();
            GameSeed = msg.ReadInt32();

        }

        /// <summary>
        /// Encode un message sortant avec les données relatives au message
        /// </summary>
        /// <param name="msg">Le message sortant</param>
        public override void EncodeMessage(NetOutgoingMessage msg)
        {
            msg.Write((byte)MessageType);
            msg.Write(GameSeed);
        }
    }
}
