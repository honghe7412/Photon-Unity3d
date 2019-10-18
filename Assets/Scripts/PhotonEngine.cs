using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;

public class PhotonEngine : Singleton<PhotonEngine>, IPhotonPeerListener
{
    private static PhotonPeer peer;
    public static PhotonPeer Peer { get { return peer; } }
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

    void IPhotonPeerListener.DebugReturn(DebugLevel level, string message)
    {

    }

    void IPhotonPeerListener.OnEvent(EventData eventData) //响应服务器端发送过来的时间
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

    void IPhotonPeerListener.OnOperationResponse(OperationResponse operationResponse) //接收服务器回应
    {
        switch (operationResponse.OperationCode)
        {
            case 1:
                Debug.Log("收到了客户端响应"+ operationResponse.OperationCode);
                Dictionary<byte, object> data = operationResponse.Parameters;
                object IntValue,StringValue; ;
                data.TryGetValue(1, out IntValue);
                data.TryGetValue(2, out StringValue);
                Debug.Log(IntValue.ToString() + " , " + StringValue.ToString());
                break;

            default:
                break;
        }
    }

    void IPhotonPeerListener.OnStatusChanged(StatusCode statusCode) //连接状态
    {

    }
}
