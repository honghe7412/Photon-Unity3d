using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test:Singleton<Test>
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SendRequest();
        }
    }

    private void SendRequest() //发送请求
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        PhotonEngine.Peer.OpCustom(1, data, true);
    }
}
