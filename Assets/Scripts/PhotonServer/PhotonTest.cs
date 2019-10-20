using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SendRequest();
        }
    }

    void SendRequest()
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>(); //所要传递的数据
        data.Add(1, 100);
        data.Add(2, "Clinet");
        PhotonEngine.Peer.OpCustom(1, data, true);
    }
}
