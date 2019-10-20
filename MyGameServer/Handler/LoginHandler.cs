using Photon.SocketServer;
using Common.Tools;
using Common;
using MyGameServer.Manage;

namespace MyGameServer.Handler
{
    class LoginHandler : BaseHandler
    {
        public LoginHandler()
        {
            OpCode = OperationCode.Login;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer)
        {
            string username = DicTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.UserName) as string;
            string password = DicTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.PassWord) as string;

            UserManage userManage = new UserManage();
            bool isSuccess = userManage.VerifiyUserAndPassword(username, password);

            OperationResponse response = new OperationResponse(operationRequest.OperationCode);

            if(isSuccess)
                response.ReturnCode=(short)ReturnCode.Success;
            else
                response.ReturnCode = (short)ReturnCode.Failed;

            peer.SendOperationResponse(response, sendParameters);
        }
    }
}
