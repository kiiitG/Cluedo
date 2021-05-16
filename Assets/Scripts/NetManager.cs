using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

public class NetManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private InputField nameOfRoom;

    [SerializeField] private Canvas nicknamePanel;
    [SerializeField] private InputField nickname;

    public void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void Start()
    {
        PhotonNetwork.Disconnect();
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.NetworkingClient.AppVersion = "1";
        PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion = "1";
    }

    public void OnCreateRoomButtonClick()
    {
        if (PhotonNetwork.InLobby)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 6;
            PhotonNetwork.JoinOrCreateRoom(nameOfRoom.text, roomOptions, PhotonNetwork.CurrentLobby);
        }
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("room is created");
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public void OnEnteredNicknameButtonClick()
    {
        if (nickname.text.Length == 0)
        {
            PhotonNetwork.LocalPlayer.NickName = "player";
            Destroy(nicknamePanel.gameObject);
        }
        else if (nickname.text.Length < 11)
        {
            PhotonNetwork.LocalPlayer.NickName = nickname.text;
            Destroy(nicknamePanel.gameObject);
        }
    }

    public void OnStartGameButtonClick()
    {
        // if (PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        PhotonNetwork.LoadLevel("Game");
    }
}
