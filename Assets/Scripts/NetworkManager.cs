using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System.Collections;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

public class NetworkManager : MonoBehaviourPunCallbacks { 
    private string roomName;
    private string userName;

    public void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void setUserName(string userName)
    {
        this.userName = userName;
    }

    public void setRoomName(string roomName)
    {
        this.roomName = "squid";
    }

    public void Connect()
    {
        //PhotonNetwork.ConnectUsingSettings();
    }

    public bool IsMasterClient()
    {
        return PhotonNetwork.IsMasterClient;
    }

    public override void OnJoinedRoom()
    {
        PhotonHashtable playerCustomProps = new PhotonHashtable();
        playerCustomProps["id"] = PhotonNetwork.CurrentRoom.PlayerCount;
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerCustomProps);
        Debug.Log("local id is " + PhotonNetwork.LocalPlayer.CustomProperties["id"]);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, PhotonHashtable changedProps)
    {
        Debug.Log("local id is " + PhotonNetwork.LocalPlayer.CustomProperties["id"]);
        PhotonNetwork.LoadLevel("Game");
    }

    public override void OnConnectedToMaster()
    {
        JoinOrCreatePrivateRoom(roomName);
    }

    public void JoinOrCreatePrivateRoom(string nameEveryFriendKnows)
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = false;
        PhotonNetwork.JoinOrCreateRoom(nameEveryFriendKnows, roomOptions, null);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("failed to join room : " + message);
    }
}
