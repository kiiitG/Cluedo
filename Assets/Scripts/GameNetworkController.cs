using UnityEngine;
using Photon.Pun;
using System;

public class GameNetworkController : MonoBehaviourPunCallbacks
{
    public void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Photon.Realtime.RoomOptions op = new Photon.Realtime.RoomOptions();
        op.MaxPlayers = 6;
        PhotonNetwork.JoinOrCreateRoom("kek", op, Photon.Realtime.TypedLobby.Default);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log("failed");
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}
