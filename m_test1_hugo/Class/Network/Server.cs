﻿using System;
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
    /// <summary>
    /// Décrit le serveur du jeu, qui va distribuer
    /// équitablement les données à tous les clients
    /// connectés.
    /// </summary>
    public class Server
    {
        #region Fields

        /// <summary>
        /// Le NetServer interne au serveur
        /// </summary>
        private NetServer gameserver;

        /// <summary>
        /// La configuration initiale du serveur
        /// </summary>
        private NetPeerConfiguration conf = new NetPeerConfiguration("TODO-game");

        /// <summary>
        /// Décrit si le serveur a démarré
        /// </summary>
        private bool hasStarted = false;

        /// <summary>
        /// Décrit si le thread du serveur devrait s'arrêter
        /// </summary>
        private bool shouldStop = false;

        /// <summary>
        /// Le thread du serveur
        /// </summary>
        private Thread svThread;

        /// <summary>
        /// La graine de génération de la carte
        /// </summary>
        private int gameSeed;

        #endregion

        #region Properties

        /// <summary>
        /// Récupère le NetServer interne au serveur
        /// </summary>
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

        /// <summary>
        /// Récupère et définit le booléen de démarrage du serveur
        /// </summary>
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

        /// <summary>
        /// Récupère et définit le thread de travail du serveur
        /// </summary>
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

        /// <summary>
        /// Récupère et définit le booléen d'arrêt du thread
        /// </summary>
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

        /// <summary>
        /// Récupère et définit la graine de génération de la carte
        /// </summary>
        public int GameSeed
        {
            get
            {
                return gameSeed;
            }

            set
            {
                gameSeed = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Construit un serveur de jeu
        /// </summary>
        public Server()
        {
            GameSeed = GenerateSeed();
            conf.MaximumConnections = 16;
            conf.Port = 12345;
            conf.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            conf.EnableMessageType(NetIncomingMessageType.UnconnectedData);

            GameServer = new NetServer(conf);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Procédure démarrant le serveur et initialisant les propriétés de démarrage
        /// </summary>
        public void Start()
        {
            GameServer.Start();
            HasStarted = true;
            System.Diagnostics.Debug.WriteLine("[INFO] Server has been created !");
            
        }

        /// <summary>
        /// La procédure principale du serveur, utilisée par le thread SvThread.
        /// Elle attend les messages des clients et les traite selon le message
        /// porté. 1ms d'arrêt.
        /// </summary>
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
                Thread.Sleep(1);
            }
            
        }

        /// <summary>
        /// Procédure qui traite les messages de type Data venant des client
        /// </summary>
        /// <param name="inc">Le message entrant</param>
        public void TreatGameMessages(NetIncomingMessage inc)
        {
            GameMessageTypes messagetype = (GameMessageTypes)inc.ReadByte();
            NetOutgoingMessage outmsg = GameServer.CreateMessage();

            switch (messagetype)
            {
                case GameMessageTypes.GetMapSeed:
                    System.Diagnostics.Debug.WriteLine("[ALERT SERVER] GETMAPSEED RECU");
                    SendMapSeed sendmapseed = new SendMapSeed(GameSeed);
                    sendmapseed.EncodeMessage(outmsg);
                    NetSendResult res =GameServer.SendMessage(outmsg, inc.SenderConnection, NetDeliveryMethod.ReliableOrdered);

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

        /// <summary>
        /// Génère la graine de génération de la carte.
        /// Est appelé au démarrage du serveur et n'est plus
        /// jamais changé.
        /// </summary>
        /// <returns>La graine de génération de la carte</returns>
        public int GenerateSeed()
        {
            return Guid.NewGuid().GetHashCode();
        }

        /// <summary>
        /// Procédure arrêtant le thread du serveur. Est
        /// appelé lorsque le jeu hôte est arrêté.
        /// </summary>
        public void RequestStop()
        {
            ShouldStop = true;
        }

        #endregion

    }
}
