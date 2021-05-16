using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerListing : MonoBehaviour
{
    [SerializeField] private Text text;
    private Player player;

    public Player Player
    {
        get
        {
            return player;
        }
        set
        {
            player = value;
            text.text = player.NickName;
            text.text += player.IsMasterClient ? " : Master" : "";
        }
    }
}
