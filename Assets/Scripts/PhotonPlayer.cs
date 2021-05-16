using UnityEngine;
using Photon.Pun;

public class PhotonPlayer : MonoBehaviour
{
    private PlayerController playerController;

    public void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    [PunRPC]
    public void MakeVersion_RPC(int[] version)
    {
        playerController.SetVersion(version);
    }

    [PunRPC]
    public void ShowCard_RPC(int card)
    {
        playerController.SetShownCard(card);
    }

    [PunRPC]
    public void Accuse_RPC(int[] version)
    {
        playerController.SetAccuseVersion(version);
    }
}
