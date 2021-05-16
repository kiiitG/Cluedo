using Photon.Pun;
using UnityEngine;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerListingMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform hover;
    [SerializeField] private PlayerListing playerListing;
    [SerializeField] private Canvas roomListingPanel;
    [SerializeField] private Button startButton;

    private List<PlayerListing> listings = new List<PlayerListing>();

    public override void OnJoinedRoom()
    {
        Destroy(roomListingPanel.gameObject);
        startButton.gameObject.SetActive(PhotonNetwork.LocalPlayer.IsMasterClient);
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            AddPlayer(PhotonNetwork.CurrentRoom.Players[i + 1]);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayer(newPlayer);
    }

    private void AddPlayer(Player player)
    {
        Debug.Log("player added");
        PlayerListing listing = Instantiate(playerListing, hover);
        if (listing != null)
        {
            listing.Player = player;
            listings.Add(listing);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("player removed");
        int index = listings.FindIndex(x => x.Player.ActorNumber == otherPlayer.ActorNumber);
        if (index != -1)
        {
            Destroy(listings[index].gameObject);
            listings.RemoveAt(index);
        }
    }
}
