using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;
using m_test1_hugo.Class.Network.Messages.Types;
using m_test1_hugo.Class.Network.Messages;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using m_test1_hugo.Class.Main.Menus.pages;

namespace m_test1_hugo.Class.Network
{
    /// <summary>
    /// Décrit le client du jeu, qui gère l'envoi de données au serveur et
    /// la réception des réponses de celui-ci
    /// </summary>
    public class Client
    {
        #region Fields

        /// <summary>
        /// Le NetClient associé au client
        /// </summary>
        private NetClient gameClient;

        /// <summary>
        /// La configuration initiale du client
        /// </summary>
        private NetPeerConfiguration conf;

        /// <summary>
        /// L'IP de l'hôte
        /// </summary>
        private string hostIP;

        /// <summary>
        /// Le port associé au client
        /// </summary>
        private int port;

        /// <summary>
        /// Décrit si le client fonctionne
        /// </summary>
        private bool isRunning = false;

        /// <summary>
        /// Décrit si le client est connecté
        /// </summary>
        private bool isConnected = false;

        /// <summary>
        /// La graine de génération aléatoire donnée par le serveur à la connexion
        /// </summary>
        private int mapSeed;

        /// <summary>
        /// Le player data associé au client
        /// </summary>
        private PlayerDataGame pdata;

        /// <summary>
        /// Le player data reçu depuis le serveur
        /// </summary>
        private PlayerDataServer recievedPlayerData;

        /// <summary>
        /// Décrit si le client devrait s'arrêter de fonctionner
        /// </summary>
        private bool shouldStop = false;

        /// <summary>
        /// Le thread de fonctionnement du client
        /// </summary>
        private Thread clThread;

        #endregion

        #region Properties

        /// <summary>
        /// Récupère le NetClient associé au client
        /// </summary>
        public NetClient GameClient
        {
            get
            {
                return gameClient;
            }

            private set
            {
                gameClient = value;
            }
        }

        /// <summary>
        /// Récupère et définit l'IP hôte
        /// </summary>
        public string HostIP
        {
            get
            {
                return hostIP;
            }

            set
            {
                hostIP = value;
            }
        }

        /// <summary>
        /// Récupère et définit le port
        /// </summary>
        public int Port
        {
            get
            {
                return port;
            }

            set
            {
                port = value;
            }
        }

        /// <summary>
        /// Récupère et définit le booléen de fonctionnement
        /// </summary>
        public bool IsRunning
        {
            get
            {
                return isRunning;
            }

            set
            {
                isRunning = value;
            }
        }

        /// <summary>
        /// Récupère et définit le booléen de connexion
        /// </summary>
        public bool IsConnected
        {
            get
            {
                return isConnected;
            }

            set
            {
                isConnected = value;
            }
        }

        /// <summary>
        /// Récupère et définit la graine de génération de la carte
        /// </summary>
        public int MapSeed
        {
            get
            {
                return mapSeed;
            }

            set
            {
                mapSeed = value;
            }
        }

        /// <summary>
        /// Récupère et définit les données du joueur
        /// </summary>
        internal PlayerDataGame Pdata
        {
            get
            {
                return pdata;
            }

            set
            {
                pdata = value;
            }
        }

        /// <summary>
        /// Récupère et définit le thread du client
        /// </summary>
        public Thread ClThread
        {
            get
            {
                return clThread;
            }

            set
            {
                clThread = value;
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
        /// Récupère et définit les données de joueur reçues depuis le serveur
        /// </summary>
        public PlayerDataServer RecievedPlayerData
        {
            get
            {
                return recievedPlayerData;
            }

            set
            {
                recievedPlayerData = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Construit un client selon l'IP et le port du serveur cible
        /// </summary>
        /// <param name="hostip">L'adresse IP du serveur</param>
        /// <param name="port">Le port du serveur</param>
        public Client(string hostip, int port)
        {
            conf = new NetPeerConfiguration("TODO-game");
            GameClient = new NetClient(conf);
            HostIP = hostip;
            Port = port;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Démarre le client, le connecte et met à jour les propriétés initiales
        /// </summary>
        public void Start()
        {
            MapSeed = 0;
            GameClient.Start();
            IsRunning = true;
            GameClient.Connect(HostIP, Port);
            IsConnected = true;
            System.Diagnostics.Debug.WriteLine("Client connecté à " + HostIP + ":" + Port);
        }

        /// <summary>
        /// La procédure principale du client, appelée par le thread ClThread.
        /// Il attend un message du serveur et le traite appropriémment.
        /// 1ms d'arrêt.
        /// </summary>
        public void HandleMessage()
        {
            while (!ShouldStop)
            {
                if (GamePage.player != null)
                {
                    FetchPlayerData();
                    SendPlayerDataToServer();
                }

                NetIncomingMessage inc;

                while ((inc = GameClient.ReadMessage()) != null)
                {
                    switch (inc.MessageType)
                    {
                        case NetIncomingMessageType.Error:

                            break;
                        case NetIncomingMessageType.StatusChanged:
                            System.Diagnostics.Debug.Write("[CLIENT STATUS] Status changed : " + inc.ReadString());
                            break;
                        case NetIncomingMessageType.UnconnectedData:
                            break;
                        case NetIncomingMessageType.ConnectionApproval:
                            break;
                        case NetIncomingMessageType.Data:
                            TreatServerMessage(inc);
                            break;
                        case NetIncomingMessageType.Receipt:
                            break;
                        case NetIncomingMessageType.DiscoveryRequest:
                            break;
                        case NetIncomingMessageType.DiscoveryResponse:
                            break;
                        case NetIncomingMessageType.VerboseDebugMessage:
                        case NetIncomingMessageType.DebugMessage:
                            System.Diagnostics.Debug.WriteLine("[CLIENT DEBUG MESSAGE] " + inc.ReadString());
                            break;
                        case NetIncomingMessageType.WarningMessage:
                            System.Diagnostics.Debug.WriteLine("[CLIENT WARNING] " + inc.ReadString());
                            break;
                        case NetIncomingMessageType.ErrorMessage:
                            System.Diagnostics.Debug.WriteLine("[CLIENT ERROR] " + inc.ReadString());
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
        /// Procédure appelée quand un message de type Data est reçu ;
        /// elle trie les messages selon leur type et les traite.
        /// </summary>
        /// <param name="inc">Le message entrant</param>
        public void TreatServerMessage(NetIncomingMessage inc)
        {
            ServerMessageTypes messageType = (ServerMessageTypes)inc.ReadByte();

            switch (messageType)
            {
                case ServerMessageTypes.SendMapSeed:
                    System.Diagnostics.Debug.WriteLine("[ALERT CLIENT] SENDMAPSEED RECU");
                    MapSeed = inc.ReadInt32();
                    break;
                case ServerMessageTypes.SendPlayerData:
                    RecievedPlayerData = new PlayerDataServer();
                    RecievedPlayerData.DecodeMessage(inc);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Arrête le thread du client. Est appelé lorsque
        /// le jeu est arrêté ou sur le menu principal
        /// </summary>
        public void RequestStop()
        {
            ShouldStop = true;
        }

        /// <summary>
        /// Récupère l'adresse IP locale de la forme
        /// 10.103.X.X, qui est l'apparence des adresses
        /// IP dans le réseau de l'IUT. A changer pour fonctionner
        /// avec tous types de réseau.
        /// </summary>
        /// <returns>Le string contenant l'adresse IP</returns>
        public static string GetLocalIPAddress()
        {
            String strHostName = string.Empty;
            // Getting Ip address of local machine...
            // First get the host name of local machine.
            strHostName = Dns.GetHostName();
            // Then using host name, get the IP address list..
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
            IPAddress[] addr = ipEntry.AddressList;
            IPAddress ip = Array.Find<IPAddress>(addr, x => x.ToString().Contains("10.103"));
            return ip.ToString();
        }
        
        /// <summary>
        /// Récupère les données du joueur de la session en vue
        /// de son envoi à chaque tick du client vers le serveur
        /// pour le distribuer aux autres clients
        /// </summary>
        public void FetchPlayerData()
        {
            Pdata = new PlayerDataGame(GamePage.player);
        }

        /// <summary>
        /// Envoie les données du joueur au serveur
        /// </summary>
        public void SendPlayerDataToServer()
        {
            NetOutgoingMessage outmsg = GameClient.CreateMessage();
            Pdata.EncodeMessage(outmsg);
            GameClient.SendMessage(outmsg, NetDeliveryMethod.ReliableOrdered);
        }

        #endregion
    }
}
