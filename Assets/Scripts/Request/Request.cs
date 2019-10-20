using Common;
using UnityEngine;
using ExitGames.Client.Photon;
public abstract class Request:MonoBehaviour
{
    public OperationCode OpCode;
    public abstract void OnOperationRequest(OperationResponse operationResponse);
    public abstract void DefaultRequest();

    protected virtual void Start()
    {
        PhotonEngine.AddRequest(this);
    }

    void OnDestroy()
    {
        PhotonEngine.RemoveRequest(this);
    }
}
