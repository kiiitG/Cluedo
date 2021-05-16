using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class RoomListing : Button
{
    [SerializeField] private Text text;
    private RoomInfo roomInfo;

    protected override void Awake()
    {
        onClick.AddListener(OnJoinRoomButtonClick);
    }

    public RoomInfo RoomInfo
    {
        get
        {
            return roomInfo;
        }
        set
        {
            roomInfo = value;
            text.text = roomInfo.Name + ": " + roomInfo.PlayerCount;
        }
    }

    public void OnJoinRoomButtonClick()
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinRoom(roomInfo.Name);
        }
    }
}
