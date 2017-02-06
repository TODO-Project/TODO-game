using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace m_test1_hugo.Class.Network.Abstract
{
    public abstract class NetworkMessage
    {
        #region Methods

        public abstract void DecodeMessage(NetIncomingMessage msg);

        public abstract void EncodeMessage(NetOutgoingMessage msg);

        #endregion
    }
}
