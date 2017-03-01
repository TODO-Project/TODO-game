using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace m_test1_hugo.Class.Network.Abstract
{
    /// <summary>
    /// Classe abstraite décrivant un message envoyé
    /// </summary>
    public abstract class NetworkMessage
    {
        #region Methods

        /// <summary>
        /// Décode un message entrant et modifie ses valeurs internes
        /// </summary>
        /// <param name="msg">Le message entrant</param>
        public abstract void DecodeMessage(NetIncomingMessage msg);

        /// <summary>
        /// Encode un message sortant avec ses propriétés internes
        /// </summary>
        /// <param name="msg">Le message sortant</param>
        public abstract void EncodeMessage(NetOutgoingMessage msg);

        #endregion
    }
}
