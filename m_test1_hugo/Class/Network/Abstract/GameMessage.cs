using m_test1_hugo.Class.Network.Messages.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m_test1_hugo.Class.Network.Abstract
{
    public abstract class GameMessage : NetworkMessage
    {
        #region Fields

        private GameMessageTypes messageType;

        #endregion

        #region Properties

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
