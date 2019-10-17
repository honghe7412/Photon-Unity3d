using System;
using System.Collections.Generic;
using System.Text;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;

namespace MyGameServer
{
    public class ClientPeer : Photon.SocketServer.ClientPeer
    {
        public ClientPeer(InitRequest initRequest):base(initRequest)
        {
            
        }
        //处理客户端断开连接请求
        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            
        }
        //操作客户端求情；
        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            switch (operationRequest.OperationCode)
            {
                case 1:
                    MyGameServer.log.Info("收到了一个客户端请求");
                    OperationResponse opResponse = new OperationResponse(operationRequest.OperationCode);
                    SendOperationResponse(opResponse,sendParameters); //给客户端一个相应
                    break;
                case 2:

                    break;

                default:
                    break;
            }
        }
    }
}
