using Lidgren.Network;
using m_test1_hugo.Class.Network.Abstract;
using m_test1_hugo.Class.Network.Messages.Types;
using m_test1_hugo.Class.Network.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Network.Messages
{
    /// <summary>
    /// Décrit un message d'envoi de données de joueur serveur => client
    /// </summary>
    public class PlayerDataServer : ServerMessage
    {
        /// <summary>
        /// Récupère et définit le type du message
        /// </summary>
        public ServerMessageTypes ServerMessageType
        {
            get; set;
        }


        /// <summary>
        /// La santé du joueur
        /// </summary>
        public int Health
        {
            get; set;
        }

        /// <summary>
        /// La santé maximale du joueur
        /// </summary>
        public int MaxHealth
        {
            get; set;
        }

        /// <summary>
        /// L'angle de rotation de la souris
        /// </summary>
        public float MouseRotationAngle
        {
            get; set;
        }

        /// <summary>
        /// La vitesse de déplacement du joueur
        /// </summary>
        public int MoveSpeed
        {
            get; set;
        }

        /// <summary>
        /// La position X du joueur
        /// </summary>
        public float PosX
        {
            get; set;
        }

        /// <summary>
        /// La position Y du joueur
        /// </summary>
        public float PosY
        {
            get; set;
        }

        /// <summary>
        /// Construit un message d'envoi de joueur
        /// </summary>
        public PlayerDataServer()
        {
            ServerMessageType = ServerMessageTypes.SendPlayerData;
        }

        /// <summary>
        /// Décode un message entrant de données
        /// </summary>
        /// <param name="msg"></param>
        public override void DecodeMessage(NetIncomingMessage msg)
        {
            //ServerMessageType = (ServerMessageTypes)msg.ReadByte();
            Health = msg.ReadInt32();
            MaxHealth = msg.ReadInt32();
            MouseRotationAngle = msg.ReadFloat();
            MoveSpeed = msg.ReadInt32();
            PosX = msg.ReadFloat();
            PosY = msg.ReadFloat();
        }

        public override void EncodeMessage(NetOutgoingMessage msg)
        {
            msg.Write((byte)ServerMessageTypes.SendPlayerData);
            msg.Write(Health);
            msg.Write(MaxHealth);
            msg.Write(MouseRotationAngle);
            msg.Write(MoveSpeed);
            msg.Write(PosX);
            msg.Write(PosY);
        }

        public override string ToString()
        {
            return "[PLAYER DATA]"
                + "\n\tHealth : " + Health + "/" + MaxHealth
                + "\n\tMouseRotationAngle : " + MouseRotationAngle
                + "\n\tMoveSpeed : " + MoveSpeed
                + "\n\tPosition : "
                    + "\n\t\tX : " + PosX
                    + "\n\t\tY : " + PosY;
        }

        public void TransferData(PlayerDataGame pdata)
        {
            Health = pdata.Health;
            MaxHealth = pdata.MaxHealth;
            MouseRotationAngle = pdata.MouseRotationAngle;
            MoveSpeed = pdata.MoveSpeed;
            PosX = pdata.PosX;
            PosY = pdata.PosY;
        }
    }
}
