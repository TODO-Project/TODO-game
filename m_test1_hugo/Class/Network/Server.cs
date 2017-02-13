using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;
using m_test1_hugo.Class.Network.Messages.Types;
using m_test1_hugo.Class.Network.Messages;
using System.Threading;

namespace m_test1_hugo.Class.Network
{
    public class Server
    {
        #region Fields

        private NetServer gameserver;
        private NetPeerConfiguration conf = new NetPeerConfiguration("TODO-game");
        private bool hasStarted = false;
        private bool shouldStop = false;
        private Thread svThread;

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

        public Thread SvThread
        {
            get
            {
                return svThread;
            }

            set
            {
                svThread = value;
            }
        }

        public bool ShouldStop
        {
            get
            {
                return shouldStop;
            }

            set
            {
                shouldStop = value;
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
        }

        #endregion

        #region Methods

        public void Start()
        {
            GameServer.Start();
            HasStarted = true;
            System.Diagnostics.Debug.WriteLine("[INFO] Server has been created !");
            
        }

        public void HandleMessages()
        {
            while (!ShouldStop)
            {
                NetIncomingMessage inc;

                while ((inc = GameServer.ReadMessage()) != null)
                {
                    switch (inc.MessageType)
                    {
                        case NetIncomingMessageType.Error:

                            break;
                        case NetIncomingMessageType.StatusChanged:
                            System.Diagnostics.Debug.Write("[STATUS] Status changed : ");
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
                                    System.Diagnostics.Debug.WriteLine(inc.SenderConnection + "is connected.");
                                    //PlayerList.Add(inc.SenderConnection);
                                    break;
                                case NetConnectionStatus.Disconnecting:
                                    System.Diagnostics.Debug.WriteLine(inc.SenderConnection + "is disconnecting...");
                                    break;
                                case NetConnectionStatus.Disconnected:
                                    System.Diagnostics.Debug.WriteLine(inc.SenderConnection + " has disconnected from the server !");
                                    //PlayerList.Remove(inc.SenderConnection);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case NetIncomingMessageType.UnconnectedData:
                            break;
                        case NetIncomingMessageType.ConnectionApproval:
                            System.Diagnostics.Debug.WriteLine("[CONNECTION APPROVAL] New connection from : " + inc.SenderConnection);
                            inc.SenderConnection.Approve();
                            System.Diagnostics.Debug.WriteLine("[CONNECTION APPROVAL] " + inc.SenderConnection + " has been approved !");
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
                            System.Diagnostics.Debug.WriteLine("[DEBUG MESSAGE] " + inc.ReadString());
                            break;
                        case NetIncomingMessageType.WarningMessage:
                            System.Diagnostics.Debug.WriteLine("[WARNING] " + inc.ReadString());
                            break;
                        case NetIncomingMessageType.ErrorMessage:
                            System.Diagnostics.Debug.WriteLine("[ERROR] " + inc.ReadString());
                            break;
                        case NetIncomingMessageType.NatIntroductionSuccess:
                            break;
                        case NetIncomingMessageType.ConnectionLatencyUpdated:
                            break;
                        default:
                            break;
                    }
                }
                Thread.Sleep(33);
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
                    foreach (NetConnection co in GameServer.Connections)
                    {
                        if (co != inc.SenderConnection)
                        {
                            GameServer.SendMessage(outmsg, co, NetDeliveryMethod.ReliableOrdered);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public void RequestStop()
        {
            ShouldStop = true;
        }

        #endregion

    }
}
