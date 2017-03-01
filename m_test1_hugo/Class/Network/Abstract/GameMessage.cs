using m_test1_hugo.Class.Network.Messages.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Network.Abstract
{
    /// <summary>
    /// Classe abstraite qui décrit un message venant du client
    /// </summary>
    public abstract class GameMessage : NetworkMessage
    {
        #region Fields

        /// <summary>
        /// Le type du message
        /// </summary>
        private GameMessageTypes messageType;

        #endregion

        #region Properties

        /// <summary>
        /// Récupère et définit le type du message
        /// </summary>
        public GameMessageTypes MessageType
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
