using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;

public class PhotonEngine : Singleton<PhotonEngine>, IPhotonPeerListener
{
    public static PhotonPeer Peer { get { return peer; } }
    private static PhotonPeer peer;

    private void Start()
    {
        //listener:接受服务器端相应
        peer = new PhotonPeer(this, ConnectionProtocol.Udp); //协议类型
        peer.Connect("127.0.0.1:4530", "MyGame1");
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (peer != null && peer.PeerState== PeerStateValue.Connected)
        {
            peer.Disconnect();
        }
    }

    private void Update()
    {
        peer.Service();
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        throw new System.NotImplementedException();
    }
    
    public void OnEvent(EventData eventData) //服务器想客户端发起请求
    {
        throw new System.NotImplementedException();
    }

    public void OnOperationResponse(OperationResponse operationResponse) //发起请求的处理
    {
        switch (operationResponse.OperationCode)
        {
            case 1:
                Debug.Log("收到了客户端相应");
                break;
            default:
                break;
        }
    }

    public void OnStatusChanged(StatusCode statusCode) //服务器连接发生改变的时候触发
    {
        Debug.Log(statusCode);
    }
}
