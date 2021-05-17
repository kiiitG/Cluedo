using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameNetworkController : MonoBehaviourPunCallbacks
{
    [SerializeField] private UIManager uiManager;

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        uiManager.OnPlayerLeftGame(otherPlayer.NickName);
    }
}
