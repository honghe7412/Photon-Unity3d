using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Common;

public class LoginRequest :Request
{
    [HideInInspector] public string UserName;
    [HideInInspector] public string Password;
    [SerializeField] private LogonScreen logonScreen;

    protected override void Start()
    {
        base.Start();

        logonScreen = FindObjectOfType<LogonScreen>();
    }

    public override void DefaultRequest() //发送请求
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add((byte)ParameterCode.UserName, UserName);
        data.Add((byte)ParameterCode.PassWord, Password);
        PhotonEngine.Peer.OpCustom((byte)OpCode, data, true);
    }
    
    public override void OnOperationRequest(OperationResponse operationResponse) //接受相应
    {
        ReturnCode returnCode = (ReturnCode)operationResponse.ReturnCode;

        logonScreen.LoginResponse(returnCode);
    }
}
