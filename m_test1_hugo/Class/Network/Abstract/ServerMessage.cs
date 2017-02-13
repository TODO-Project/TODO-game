using m_test1_hugo.Class.Network.Messages.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Network.Abstract
{
    public abstract class ServerMessage : NetworkMessage
    {
        #region Fields

        private ServerMessageTypes messageType;

        #endregion

        #region Properties

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
