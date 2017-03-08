using Lidgren.Network;
using m_test1_hugo.Class.Network.Abstract;
using m_test1_hugo.Class.Network.Messages.Types;
using m_test1_hugo.Class.Network.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using m_test1_hugo.Class.Main;
using Microsoft.Xna.Framework;

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
        /// L'orientation du joueur
        /// </summary>
        public int Row
        {
            get; set;
        }

        /// <summary>
        /// L'animation du joueur
        /// </summary>
        public int Column
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
        /// L'ID du joueur
        /// </summary>
        public long ID
        {
            get; set;
        }

        public string Pseudo
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
            Row = msg.ReadInt32();
            Column = msg.ReadInt32();
            MoveSpeed = msg.ReadInt32();
            PosX = msg.ReadFloat();
            PosY = msg.ReadFloat();
            ID = msg.ReadInt64();
            Pseudo = msg.ReadString();
        }

        public override void EncodeMessage(NetOutgoingMessage msg)
        {
            msg.Write((byte)ServerMessageTypes.SendPlayerData);
            msg.Write(Health);
            msg.Write(MaxHealth);
            msg.Write(Row);
            msg.Write(Column);
            msg.Write(MoveSpeed);
            msg.Write(PosX);
            msg.Write(PosY);
            msg.Write(ID);
            msg.Write(Pseudo);
        }

        public override string ToString()
        {
            return "[PLAYER DATA " + ID + "] " + Pseudo
                + "\n\tHealth : " + Health + "/" + MaxHealth
                + "\n\tMoveSpeed : " + MoveSpeed
                + "\n\tPosition : "
                    + "\n\t\tX : " + PosX
                    + "\n\t\tY : " + PosY;
        }

        public void TransferData(PlayerDataGame pdata)
        {
            Health = pdata.Health;
            MaxHealth = pdata.MaxHealth;
            Row = pdata.Row;
            Column = pdata.Column;
            MoveSpeed = pdata.MoveSpeed;
            PosX = pdata.PosX;
            PosY = pdata.PosY;
            ID = pdata.ID;
            Pseudo = pdata.Pseudo;
        }

        public void TransferDataToPlayer(Player p)
        {
            p.Health = Health;
            p.MaxHealth = MaxHealth;
            p.currentRow = Row;
            p.currentColumn = Column;
            p.MoveSpeed = MoveSpeed;
            Vector2 pos = new Vector2(PosX, PosY);
            p.Position = pos;
            p.Pseudo = Pseudo;
        }
    }
}
