using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;

namespace MyGameServer.Handler
{
    abstract class BaseHandler
    {
        public OperationCode OpCode;
        public abstract void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer);
    }
}
