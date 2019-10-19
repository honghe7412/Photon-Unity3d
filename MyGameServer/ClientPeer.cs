using System;
using System.Collections.Generic;
using System.Text;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using MyGameServer.Manage;

namespace MyGameServer
{
    class ClientPeer : Photon.SocketServer.ClientPeer
    {
        public ClientPeer(InitRequest initRequest):base(initRequest)
        {

        }

        //客户端连接关闭关闭todo......
        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {

        }

        //操作客户端请求 TODO OnOperationRequest......
        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            switch (operationRequest.OperationCode)
            {
                case 1:
                    MyGameServer.log.Info("收到了一个客户端请求");
                    Dictionary<byte, object> data = operationRequest.Parameters; //客户端传回的数据参数；

                    object IntValue;
                    data.TryGetValue(1, out IntValue);
                    object StringValue;
                    data.TryGetValue(2, out StringValue);
                    MyGameServer.log.Info("得到的参数: " + IntValue.ToString() + "," + StringValue.ToString());

                    OperationResponse opResponse = new OperationResponse(operationRequest.OperationCode);

                    Dictionary<byte, object> data2 = new Dictionary<byte, object>(); //所要传递的数据

                    IUserManage user = new UserManage();

                    data2.Add(1, user.GetById(1).Username);
                    data2.Add(2, user.GetById(1).Password);

                    opResponse.SetParameters(data2); //向客户端传递数据；
                    SendOperationResponse(opResponse, sendParameters); //给客户端一个响应 必须有请求，才能有响应；
                    
                    EventData ed = new EventData(1); //1代表事件代码；
                    ed.SetParameters(data2);
                    SendEvent(ed,new SendParameters());  //向客户端发送数据和SendOperationResponse有区别，可以不必有响应；

                    break;
                case 2:

                    break;

                default:
                    break;
            }
        }
    }
}
