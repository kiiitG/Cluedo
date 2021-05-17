using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class RoomListingMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform hover;
    [SerializeField] private RoomListing roomListing;

    private List<RoomListing> listings = new List<RoomListing>();

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (hover != null) {
           hover.DetachChildren();
        }
        listings.Clear();
        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
            {
                int index = listings.FindIndex(x => x.RoomInfo.Name.Equals(roomList[i]));
                if (index != -1)
                {
                    Destroy(listings[index].gameObject);
                    listings.RemoveAt(index);
                }
            }
            else
            {
                RoomListing listing = Instantiate(roomListing, hover);
                if (listing != null)
                {
                    listing.RoomInfo = roomList[i];
                    listings.Add(listing);
                }
            }
        }
    }
}
