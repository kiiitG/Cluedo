using System;
using Photon.Pun;
using UnityEngine;

public class SinglePlayersQueue
{
    public Action TurnChanged;

    private int currentId;
    private int count;
    private int counter;

    public SinglePlayersQueue(int currentId, Action handler)
    {
        TurnChanged += handler;
        Debug.Log("TurnChanged.Method " + TurnChanged.Method);
        count = PhotonNetwork.CurrentRoom.PlayerCount;
        this.currentId = currentId;
        counter = 0;
    }

    public int Current
    {
        get
        {
            return currentId;
        }
    }

    public bool Next()
    {
        Debug.Log("single queue next " + currentId);
        if (counter == count - 1)
        {
            Debug.Log("single queue return false");
            return false;
        }

        currentId = (currentId % count) + 1;
        counter++;
        Debug.Log("single queue next " + currentId);
        TurnChanged?.Invoke();
        return true;
    }
}
