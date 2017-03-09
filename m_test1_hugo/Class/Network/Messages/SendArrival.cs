using m_test1_hugo.Class.Network.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;
using m_test1_hugo.Class.Main.Menus.pages;

namespace m_test1_hugo.Class.Network.Messages
{
    /// <summary>
    /// Décrit un message d'arrivée au serveur
    /// </summary>
    public class SendArrival : GameMessage
    {
        /// <summary>
        /// Récupère et définit le pseudo du joueur
        /// </summary>
        public string Pseudo
        {
            get; set;
        }

        /// <summary>
        /// Le numéro de la team
        /// </summary>
        public int TeamNumber
        {
            get; set;
        }

        /// <summary>
        /// Récupère et définit l'ID du joueur
        /// </summary>
        public long ID
        {
            get; set;
        }

        /// <summary>
        /// L'arme du joueur
        /// </summary>
        public string Weapon
        {
            get; set;
        }

        /// <summary>
        /// Construit un message d'arrivée au serveur
        /// </summary>
        public SendArrival()
        {
            MessageType = Types.GameMessageTypes.SendArrival;
            ID = GamePage.unique_ID;

        }

        /// <summary>
        /// Construit un message d'arrivée au serveur en précisant le pseudo du joueur
        /// </summary>
        /// <param name="pseudo">Le pseudo du joueur</param>
        public SendArrival(string pseudo, int teamNumber, string weapon) : this()
        {
            Pseudo = pseudo;
            TeamNumber = teamNumber;
            Weapon = weapon;
        }

        /// <summary>
        /// Décode un message d'arrivée du serveur depuis un message entrant
        /// </summary>
        /// <param name="msg">Un message entrant</param>
        public override void DecodeMessage(NetIncomingMessage msg)
        {
            //MessageType = (Types.GameMessageTypes)msg.ReadByte();
            Pseudo = msg.ReadString();
            ID = msg.ReadInt64();
            TeamNumber = msg.ReadInt32();
            Weapon = msg.ReadString();
        }

        /// <summary>
        /// Encode un message entrant avec les données d'un message d'arrivée au serveur
        /// </summary>
        /// <param name="msg">Un message sortant</param>
        public override void EncodeMessage(NetOutgoingMessage msg)
        {
            msg.Write((byte)MessageType);
            msg.Write(Pseudo);
            msg.Write(ID);
            msg.Write(TeamNumber);
            msg.Write(Weapon);
        }
    }
}
