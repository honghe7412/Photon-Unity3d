using Photon.SocketServer;
using Common.Tools;
using Common;
using MyGameServer.Manage;
using MyGameServer.Model;
using System;

namespace MyGameServer.Handler
{
    class RegisterHandler : BaseHandler
    {
        public RegisterHandler()
        {
            OpCode = OperationCode.Register;
        }
        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer)
        {
            string username = DicTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.UserName) as string;
            string password = DicTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.PassWord) as string;

            UserManage userManage = new UserManage();
            Runoob_tbl user = userManage.GetByUserName(username);

            OperationResponse response = new OperationResponse(operationRequest.OperationCode);

            if (user==null) //没有用户才注册
            {
                user = new Runoob_tbl() { Username = username, Password = password, RegisterDate = DateTime.Now };
                userManage.Add(user);
                response.ReturnCode = (short)ReturnCode.Success;
            }
            else
                response.ReturnCode = (short)ReturnCode.Failed;

            peer.SendOperationResponse(response, sendParameters);
        }
    }
}
