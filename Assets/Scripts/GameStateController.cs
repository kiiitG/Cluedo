using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using ExitGames.Client.Photon;

public delegate void GameStateHandler(GameState newState);

public enum GameState {
   Start, Play, Finish
}

public class GameStateController : MonoBehaviour, IOnEventCallback
{
    protected const byte SET_GAME_STATE_EVENT_CODE = 1;

    [SerializeField] private PlayerController playerController;

    private CyclicPlayersQueue queue;

    private GameState currentState;

    public void Awake()
    {
        SetDependencies();
    }

    public void Start()
    {
        if (!PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            return;
        }
        OnGameStateChanged(GameState.Start);
    }

    private void SetDependencies()
    {
        queue = new CyclicPlayersQueue(0, OnGameStateChanged);
        playerController.PlayerFinishedTurn += OnPlayerFinishedTurn;
        playerController.PlayerMadeAccuse += OnPlayerMadeAccuse;
    }

    private void OnPlayerFinishedTurn()
    {
        print("OnPlayerFinishedTurn");
        queue.Next();
    }

    private void ProcessNewGameState()
    {
        /*
        Debug.Log("process new game state: " + currentState);
        if (currentState == GameState.Start)
        {
            queue.Next();
        }
        else if (currentState == GameState.Play)
        {
            playerController.SetTurn(queue.Current);
        }
        else if (currentState == GameState.Finish)
        {
            playerController.SetInactive();
        }
        */
    }

    public void OnPlayerMadeAccuse(int id, bool isRight)
    {
        if (isRight)
        {
            OnGameStateChanged(GameState.Finish);
        }
        else
        {
            queue.SetInactive(id);
        }
    }

    private void OnGameStateChanged(GameState newState)
    {
        object[] content = new object[] { (int)newState };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(SET_GAME_STATE_EVENT_CODE, content, raiseEventOptions, SendOptions.SendReliable);
    }

    public void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;
        if (eventCode == SET_GAME_STATE_EVENT_CODE)
        {
            object[] data = (object[])photonEvent.CustomData;
            currentState = (GameState)data[0];
            ProcessNewGameState();
        }
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
