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
    public class PlayerDataGame : GameMessage
    {
        public GameMessageTypes GameMessageType
        {
            get; set;
        }

        public int Health
        {
            get; set;
        }

        public int MaxHealth
        {
            get; set;
        }

        public float MouseRotationAngle
        {
            get; set;
        }

        public int MoveSpeed
        {
            get; set;
        }

        public float PosX
        {
            get; set;
        }

        public float PosY
        {
            get; set;
        }

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

        public PlayerDataGame()
        {
            GameMessageType = GameMessageTypes.SendPlayerData;
        }

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
