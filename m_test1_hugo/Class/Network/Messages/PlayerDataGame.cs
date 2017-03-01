using Lidgren.Network;
using m_test1_hugo.Class.Main;
using m_test1_hugo.Class.Network.Abstract;
using m_test1_hugo.Class.Network.Messages.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Network.Messages
{
    /// <summary>
    /// Décrit un message d'envoi de données joueurs client => serveur
    /// </summary>
    public class PlayerDataGame : GameMessage
    {
        /// <summary>
        /// Récupère et définit le type du message
        /// </summary>
        public GameMessageTypes GameMessageType
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
        /// Construit un message d'envoi de joueur en précisant un joueur
        /// </summary>
        /// <param name="p">Le joueur dont il faut extraire les données</param>
        public PlayerDataGame(Player p)
        {
            GameMessageType = GameMessageTypes.SendPlayerData;
            Health = p.Health;
            MaxHealth = p.MaxHealth;
            MouseRotationAngle = p.MouseRotationAngle;
            MoveSpeed = p.MoveSpeed;
            PosX = p.Position.X;
            PosY = p.Position.Y;
        }

        /// <summary>
        /// Construit un message d'envoi de joueur vide
        /// </summary>
        public PlayerDataGame()
        {
            GameMessageType = GameMessageTypes.SendPlayerData;
        }

        /// <summary>
        /// Décode un message d'envoi du joueur
        /// </summary>
        /// <param name="msg">Le message entrant</param>
        public override void DecodeMessage(NetIncomingMessage msg)
        {
            //GameMessageType = (GameMessageTypes)msg.ReadByte();
            Health = msg.ReadInt32();
            MaxHealth = msg.ReadInt32();
            MouseRotationAngle = msg.ReadFloat();
            MoveSpeed = msg.ReadInt32();
            PosX = msg.ReadFloat();
            PosY = msg.ReadFloat();
        }

        /// <summary>
        /// Encode un message d'envoi de joueur
        /// </summary>
        /// <param name="msg">Le message sortant</param>
        public override void EncodeMessage(NetOutgoingMessage msg)
        {
            msg.Write((byte)GameMessageTypes.SendPlayerData);
            msg.Write(Health);
            msg.Write(MaxHealth);
            msg.Write(MouseRotationAngle);
            msg.Write(MoveSpeed);
            msg.Write(PosX);
            msg.Write(PosY);
        }

        /// <summary>
        /// Permet d'afficher un message d'envoi de joueur (DEBUG)
        /// </summary>
        /// <returns>Un string décrivant le joueur</returns>
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

    }
}
