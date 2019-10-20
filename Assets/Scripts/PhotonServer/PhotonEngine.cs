using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Common;

public class PhotonEngine : Singleton<PhotonEngine>, IPhotonPeerListener
{
    private static PhotonPeer peer;
    public static PhotonPeer Peer { get { return peer; } }

    private static Dictionary<OperationCode, Request> RequestDict = new Dictionary<OperationCode, Request>();
    private void Start()
    {
        //链接服务器
        peer = new PhotonPeer(this, ConnectionProtocol.Udp);
        peer.Connect("127.0.0.1:5055", "MyGame1");
    }

    private void Update()
    {
        peer.Service();
    }

    protected override void OnDestroy()
    {
        //base.OnDestroy();
        if (peer != null && peer.PeerState == PeerStateValue.Connected)
        {
            peer.Disconnect(); //程序退出，关闭连接
        }
    }

    public void DebugReturn(DebugLevel level, string message)
    {

    }

    public void OnEvent(EventData eventData) //响应服务器端发送过来的时间
    {
        switch (eventData.Code)
        {
            case 1:
                Dictionary<byte, object> data = eventData.Parameters;
                object IntValue, StringValue; ;
                data.TryGetValue(1, out IntValue);
                data.TryGetValue(2, out StringValue);
                Debug.Log("Event." + IntValue.ToString() + " , " + "Event." + StringValue.ToString());
                break;

            default:
                break;
        }
    }

    public void OnOperationResponse(OperationResponse operationResponse) //接收服务器回应
    {
        OperationCode opCode = (OperationCode)operationResponse.OperationCode;
        bool temp = RequestDict.TryGetValue(opCode, out Request request);

        if (temp)
        {
            request.OnOperationRequest(operationResponse); //处理回应
        }
        else
        {
            Debug.Log("not Response");
        }
    }

    public void OnStatusChanged(StatusCode statusCode) //连接状态
    {

    }

    public static void AddRequest(Request request)
    {
        RequestDict.Add(request.OpCode, request);
    }

    public static void RemoveRequest(Request request)
    {
        RequestDict.Remove(request.OpCode);
    }
}
