using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System;
using System.Collections;

public class CardCollection : MonoBehaviour, IOnEventCallback
{
    protected const byte SHUFFLE_CARDS_EVENT_CODE = 2;

    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private PlayerController playerController;
    private GameObject[] cards;
    private int[] order;
    [SerializeField] private Transform cardPanel;

    public void Awake()
    {
        Shuffle();
    }

    public void Start()
    {
        cards = new GameObject[21];
        for (int i = 0; i < prefabs.Length; i++)
        {
            cards[i] = Instantiate(prefabs[i], cardPanel);
        }
    }

    public GameObject this[int index]
    {
        get
        {
            return cards[index];
        }
    }

    public GameObject[] GetRightVersion()
    {
        return new GameObject[] { cards[order[18]], cards[order[19]], cards[order[20]] };
    }

    private void Shuffle()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        int[] order = new int[21];
        order[18] = UnityEngine.Random.Range(0, 6);
        order[19] = UnityEngine.Random.Range(6, 12);
        order[20] = UnityEngine.Random.Range(12, 21);
        int k = 0;
        for (int i = 0; i < 21; i++)
        {
            if (i != order[18] && i!= order[19] && i != order[20])
            {
                order[k] = i;
                k++;
            }
        }
        int tmp, j;
        for (int i = 17; i >= 0; i--)
        {
            j = UnityEngine.Random.Range(0, 17);
            tmp = order[i];
            order[i] = order[j];
            order[j] = tmp;
        }
        object[] content = new object[] { order };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(SHUFFLE_CARDS_EVENT_CODE, content, raiseEventOptions, SendOptions.SendReliable);
    }

    public void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;
        if (eventCode == SHUFFLE_CARDS_EVENT_CODE)
        {
            object[] data = (object[])photonEvent.CustomData;
            this.order = (int[])data[0];
            playerController.SetCards(order);
            Debug.Log("cards order is set");
            string log = "";
            for (int i = 0; i < order.Length; i++)
            {
                log += cards[order[i]].GetComponent<Card>().GetName() + "\n";
            }
            Debug.Log(log);
        }
    }

    public bool IsAccuseRight(int[] version)
    {
        return order[18] == version[0] && order[19] == version[1] && order[20] == version[2];
    }

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
}
