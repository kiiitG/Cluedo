using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public delegate void TurnSetter(int id);

public class CyclicPlayersQueue
{
    public GameStateHandler TurnChanged;
    public TurnSetter turnSetter;

    private int currentId;
    private int count;
    List<int> inactive;

    public CyclicPlayersQueue(int currentId, TurnSetter handler)
    {
        turnSetter += handler;
        count = PhotonNetwork.CurrentRoom.PlayerCount;
        this.currentId = currentId;
        inactive = new List<int>();
    }

    public CyclicPlayersQueue(int currentId, GameStateHandler handler)
    {
        TurnChanged += handler;
        count = PhotonNetwork.CurrentRoom.PlayerCount;
        this.currentId = currentId;
        inactive = new List<int>();
    }

    public int Current
    {
        get
        {
            return currentId;
        }
    }

    public void SetInactive(int id)
    {
        inactive.Add(id);
    }

    public void Next()
    {
        do
        {
            currentId = (currentId % count) + 1;
        } while (inactive.Contains(currentId));
        Debug.Log("queue next, current id = " + currentId);
        TurnChanged?.Invoke(GameState.Play);
        turnSetter?.Invoke(Current);
    }
}
