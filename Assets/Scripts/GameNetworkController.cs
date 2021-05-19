using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class GameNetworkController : MonoBehaviourPunCallbacks
{
    [SerializeField] private UIManager uiManager;

    public void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = false;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        uiManager.OnDisconnected();
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        SceneManager.LoadScene("MultiplayerMenu");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        uiManager.OnPlayerLeftGame(otherPlayer.NickName);
    }
}
