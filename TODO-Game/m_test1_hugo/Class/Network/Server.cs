using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;
using m_test1_hugo.Class.Network.Messages.Types;
using Server.Network.Messages;
using m_test1_hugo.Class.Network.Messages;

namespace m_test1_hugo.Class.Network
{
    public class Server
    {
        #region Fields

        private NetServer gameserver;
        private NetPeerConfiguration conf = new NetPeerConfiguration("TODO-game");
        private bool hasStarted = false;

        #endregion

        #region Properties

        public NetServer GameServer
        {
            get
            {
                return gameserver;
            }

            private set
            {
                gameserver = value;
            }
        }

        public bool HasStarted
        {
            get
            {
                return hasStarted;
            }

            set
            {
                hasStarted = value;
            }
        }

        #endregion

        #region Constructors

        public Server()
        {
            conf.MaximumConnections = 16;
            conf.Port = 12345;
            conf.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            conf.EnableMessageType(NetIncomingMessageType.UnconnectedData);

            GameServer = new NetServer(conf);
            GameServer.Start();
            HasStarted = true;
            Console.WriteLine("[INFO] Server has been created !");
        }

        #endregion

        #region Methods

        public void HandleMessages()
        {
            NetIncomingMessage inc;

            while ((inc = GameServer.ReadMessage()) != null)
            {
                switch (inc.MessageType)
                {
                    case NetIncomingMessageType.Error:

                        break;
                    case NetIncomingMessageType.StatusChanged:
                        Console.Write("[STATUS] Status changed : ");
                        switch ((NetConnectionStatus)inc.ReadByte())
                        {
                            case NetConnectionStatus.None:
                                break;
                            case NetConnectionStatus.InitiatedConnect:
                                break;
                            case NetConnectionStatus.ReceivedInitiation:
                                break;
                            case NetConnectionStatus.RespondedAwaitingApproval:
                                break;
                            case NetConnectionStatus.RespondedConnect:
                                break;
                            case NetConnectionStatus.Connected:
                                Console.WriteLine(inc.SenderConnection + "is connected.");
                                //PlayerList.Add(inc.SenderConnection);
                                break;
                            case NetConnectionStatus.Disconnecting:
                                Console.WriteLine(inc.SenderConnection + "is disconnecting...");
                                break;
                            case NetConnectionStatus.Disconnected:
                                Console.WriteLine(inc.SenderConnection + " has disconnected from the server !");
                                //PlayerList.Remove(inc.SenderConnection);
                                break;
                            default:
                                break;
                        }
                        break;
                    case NetIncomingMessageType.UnconnectedData:
                        break;
                    case NetIncomingMessageType.ConnectionApproval:
                        Console.WriteLine("[CONNECTION APPROVAL] New connection from : " + inc.SenderConnection);
                        inc.SenderConnection.Approve();
                        Console.WriteLine("[CONNECTION APPROVAL] " + inc.SenderConnection + " has been approved !");
                        break;
                    case NetIncomingMessageType.Data:
                        TreatGameMessages(inc);
                        break;
                    case NetIncomingMessageType.Receipt:
                        break;
                    case NetIncomingMessageType.DiscoveryRequest:
                        break;
                    case NetIncomingMessageType.DiscoveryResponse:
                        break;
                    case NetIncomingMessageType.VerboseDebugMessage:
                    case NetIncomingMessageType.DebugMessage:
                        Console.WriteLine("[DEBUG MESSAGE] " + inc.ReadString());
                        break;
                    case NetIncomingMessageType.WarningMessage:
                        Console.WriteLine("[WARNING] " + inc.ReadString());
                        break;
                    case NetIncomingMessageType.ErrorMessage:
                        Console.WriteLine("[ERROR] " + inc.ReadString());
                        break;
                    case NetIncomingMessageType.NatIntroductionSuccess:
                        break;
                    case NetIncomingMessageType.ConnectionLatencyUpdated:
                        break;
                    default:
                        break;
                }
            }
        }

        public void TreatGameMessages(NetIncomingMessage inc)
        {
            GameMessageTypes messagetype = (GameMessageTypes)inc.ReadByte();
            NetOutgoingMessage outmsg = GameServer.CreateMessage();

            switch (messagetype)
            {
                case GameMessageTypes.RequestMapSeed:
                    break;
                case GameMessageTypes.SendPlayerData:
                    PlayerDataGame playerdatagame = new PlayerDataGame();
                    playerdatagame.DecodeMessage(inc);
                    PlayerDataServer playerdataserver = new PlayerDataServer();
                    playerdataserver.TransferData(playerdatagame);
                    playerdataserver.EncodeMessage(outmsg);
                    GameServer.SendToAll(outmsg, NetDeliveryMethod.ReliableOrdered);
                    break;
                default:
                    break;
            }
        }

        #endregion

    }
}
