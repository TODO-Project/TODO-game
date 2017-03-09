using m_test1_hugo.Class.Network.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace m_test1_hugo.Class.Network.Messages
{
    public class NewBulletGame : GameMessage
    {
        /// <summary>
        /// L'ID du joueur qui a tiré
        /// </summary>
        public long PlayerID
        {
            get; set;
        }

        public float AngleTir
        {
            get; set;
        }


        public NewBulletGame()
        {
            MessageType = Types.GameMessageTypes.NewBulletGame;
        }

        public NewBulletGame(long ID, float angleTir)
            : this()
        {
            PlayerID = ID;
            AngleTir = angleTir;
        }

        public override void DecodeMessage(NetIncomingMessage msg)
        {
            PlayerID = msg.ReadInt64();
            AngleTir = msg.ReadFloat();
        }

        public override void EncodeMessage(NetOutgoingMessage msg)
        {
            msg.Write((byte)MessageType);
            msg.Write(PlayerID);
            msg.Write(AngleTir);
        }
    }
}
