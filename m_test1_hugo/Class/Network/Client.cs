﻿using System;
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

namespace m_test1_hugo.Class.Network
{
    public class Client
    {
        #region Fields

        private NetClient gameClient;
        private NetPeerConfiguration conf;
        private string hostIP;
        private int port;
        private bool isRunning = false;
        private bool isConnected = false;
        private int mapSeed;
        private PlayerDataServer pdata;
        private bool shouldStop = false;
        private Thread clThread;

        #endregion

        #region Properties

        public NetClient GameClient
        {
            get
            {
                return gameClient;
            }

            set
            {
                gameClient = value;
            }
        }

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

        internal PlayerDataServer Pdata
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

        public Client(string hostip, int port)
        {
            conf = new NetPeerConfiguration("TODO-game");
            GameClient = new NetClient(conf);
            HostIP = hostip;
            Port = port;
        }

        #endregion

        #region Methods

        public void Start()
        {
            GameClient.Start();
            IsRunning = true;
            GameClient.Connect(HostIP, Port);
            IsConnected = true;
            System.Diagnostics.Debug.WriteLine("Client connecté à " + HostIP + ":" + Port);
        }

        public void HandleMessage()
        {
            while (!ShouldStop)
            {
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

        public void TreatServerMessage(NetIncomingMessage inc)
        {
            ServerMessageTypes messageType = (ServerMessageTypes)inc.ReadByte();

            switch (messageType)
            {
                case ServerMessageTypes.SendMapSeed:
                    MapSeed = inc.ReadInt32();
                    break;
                case ServerMessageTypes.SendPlayerData:
                    Pdata = new PlayerDataServer();
                    Pdata.DecodeMessage(inc);
                    break;
                default:
                    break;
            }
        }

        public void RequestStop()
        {
            ShouldStop = true;
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }

        #endregion
    }
}
