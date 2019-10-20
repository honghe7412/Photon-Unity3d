using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using Common;

public class RegisterRequest:Request
{
    [HideInInspector] public string UserName;
    [HideInInspector] public string Password;
    [SerializeField] private LogonScreen logonScreen;
    public override void DefaultRequest()
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add((byte)ParameterCode.UserName, UserName);
        data.Add((byte)ParameterCode.PassWord, Password);
        PhotonEngine.Peer.OpCustom((byte)OpCode, data, true);
    }

    public override void OnOperationRequest(OperationResponse operationResponse)
    {
        ReturnCode returnCode = (ReturnCode)operationResponse.ReturnCode;

        logonScreen.RegisterResponse(returnCode);
    }
}
