using m_test1_hugo.Class.Network.Messages.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Network.Abstract
{
    /// <summary>
    /// Classe abstraite qui décrit un message venant du serveur
    /// </summary>
    public abstract class ServerMessage : NetworkMessage
    {
        #region Fields

        /// <summary>
        /// Le type du message
        /// </summary>
        private ServerMessageTypes messageType;

        #endregion

        #region Properties

        /// <summary>
        /// Récupère et définit le type du message
        /// </summary>
        public ServerMessageTypes MessageType
        {
            get
            {
                return messageType;
            }

            set
            {
                messageType = value;
            }
        }

        #endregion
    }
}
