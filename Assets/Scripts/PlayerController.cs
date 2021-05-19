using UnityEngine;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public delegate void Desicion(int id, bool isRight);

public class PlayerController : MonoBehaviour, CluedoPlayer
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private ClueTable table;
    [SerializeField] private CardCollection cardCollection;

    List<Vector3Int> outerCells = new List<Vector3Int>();
    List<Vector3Int> doors = new List<Vector3Int>();
    Dictionary<string, Vector3Int> rooms = new Dictionary<string, Vector3Int>();
    Dictionary<string, List<Vector3Int>> roomsToDoors = new Dictionary<string, List<Vector3Int>>();
    string[] roomName = new string[] { "Study", "Library", "Billiard Room", "Conservatory",
    "Hall", "Ball Room", "Kitchen", "Dining Room", "Lounge" };
    string[] nicknames;

    private void FillOuterCells()
    {
        outerCells.Add(new Vector3Int(-12, 7, 0));
        outerCells.Add(new Vector3Int(-12, -7, 0));
        outerCells.Add(new Vector3Int(-11, 8, 0));
        outerCells.Add(new Vector3Int(-11, 7, 0));
        outerCells.Add(new Vector3Int(-11, 1, 0));
        outerCells.Add(new Vector3Int(-11, -6, 0));
        outerCells.Add(new Vector3Int(-11, -7, 0));
        outerCells.Add(new Vector3Int(-10, 8, 0));
        outerCells.Add(new Vector3Int(-10, 7, 0));
        outerCells.Add(new Vector3Int(-10, 1, 0));
        outerCells.Add(new Vector3Int(-10, -6, 0));
        outerCells.Add(new Vector3Int(-10, -7, 0));
        outerCells.Add(new Vector3Int(-9, 8, 0));
        outerCells.Add(new Vector3Int(-9, 7, 0));
        outerCells.Add(new Vector3Int(-9, 1, 0));
        outerCells.Add(new Vector3Int(-9, -6, 0));
        outerCells.Add(new Vector3Int(-9, -7, 0));
        outerCells.Add(new Vector3Int(-8, 8, 0));
        outerCells.Add(new Vector3Int(-8, 7, 0));
        outerCells.Add(new Vector3Int(-8, 1, 0));
        outerCells.Add(new Vector3Int(-8, -6, 0));
        outerCells.Add(new Vector3Int(-8, -7, 0));
        outerCells.Add(new Vector3Int(-7, 8, 0));
        outerCells.Add(new Vector3Int(-7, 7, 0));
        outerCells.Add(new Vector3Int(-7, 1, 0));
        outerCells.Add(new Vector3Int(-7, -6, 0));
        outerCells.Add(new Vector3Int(-7, -7, 0));
        outerCells.Add(new Vector3Int(-7, -8, 0));
        outerCells.Add(new Vector3Int(-6, 8, 0));
        outerCells.Add(new Vector3Int(-6, 7, 0));
        outerCells.Add(new Vector3Int(-6, 6, 0));
        outerCells.Add(new Vector3Int(-6, 2, 0));
        outerCells.Add(new Vector3Int(-6, 1, 0));
        outerCells.Add(new Vector3Int(-6, -1, 0));
        outerCells.Add(new Vector3Int(-6, -2, 0));
        outerCells.Add(new Vector3Int(-6, -3, 0));
        outerCells.Add(new Vector3Int(-6, -4, 0));
        outerCells.Add(new Vector3Int(-6, -5, 0));
        outerCells.Add(new Vector3Int(-6, -6, 0));
        outerCells.Add(new Vector3Int(-6, -7, 0));
        outerCells.Add(new Vector3Int(-6, -8, 0));
        outerCells.Add(new Vector3Int(-6, -9, 0));
        outerCells.Add(new Vector3Int(-6, -10, 0));
        outerCells.Add(new Vector3Int(-6, -11, 0));
        for (int i = -12; i <= 12; i++)
        {
            outerCells.Add(new Vector3Int(-5, i, 0));
        }
        for (int i = -5; i <= 11; i++)
        {
            outerCells.Add(new Vector3Int(-4, i, 0));
        }
        outerCells.Add(new Vector3Int(-4, -12, 0));
        outerCells.Add(new Vector3Int(-4, -13, 0));
        outerCells.Add(new Vector3Int(-3, 5, 0));
        outerCells.Add(new Vector3Int(-3, -4, 0));
        outerCells.Add(new Vector3Int(-3, -5, 0));
        outerCells.Add(new Vector3Int(-3, -12, 0));
        outerCells.Add(new Vector3Int(-3, -13, 0));
        outerCells.Add(new Vector3Int(-2, 5, 0));
        outerCells.Add(new Vector3Int(-2, -4, 0));
        outerCells.Add(new Vector3Int(-2, -5, 0));
        outerCells.Add(new Vector3Int(-1, 5, 0));
        outerCells.Add(new Vector3Int(-1, -4, 0));
        outerCells.Add(new Vector3Int(-1, -5, 0));
        outerCells.Add(new Vector3Int(1, 5, 0));
        outerCells.Add(new Vector3Int(1, -4, 0));
        outerCells.Add(new Vector3Int(1, -5, 0));
        outerCells.Add(new Vector3Int(2, 5, 0));
        outerCells.Add(new Vector3Int(2, -4, 0));
        outerCells.Add(new Vector3Int(2, -5, 0));
        for (int i = -5; i <= 5; i++)
        {
            outerCells.Add(new Vector3Int(3, i, 0));
        }
        outerCells.Add(new Vector3Int(3, -12, 0));
        outerCells.Add(new Vector3Int(3, -13, 0));
        for (int i = -5; i <= 11; i++)
        {
            outerCells.Add(new Vector3Int(4, i, 0));
        }
        outerCells.Add(new Vector3Int(4, -12, 0));
        for (int i = -12; i <= 12; i++)
        {
            outerCells.Add(new Vector3Int(5, i, 0));
        }
        outerCells.Add(new Vector3Int(6, 6, 0));
        outerCells.Add(new Vector3Int(6, 5, 0));
        outerCells.Add(new Vector3Int(6, 4, 0));
        for (int i = -11; i <= -4; i++)
        {
            outerCells.Add(new Vector3Int(6, i, 0));
        }
        for (int i = 4; i <= 6; i++)
        {
            outerCells.Add(new Vector3Int(7, i, 0));
            outerCells.Add(new Vector3Int(7, -i, 0));
            outerCells.Add(new Vector3Int(8, i, 0));
            outerCells.Add(new Vector3Int(8, -i, 0));
            outerCells.Add(new Vector3Int(9, i, 0));
            outerCells.Add(new Vector3Int(10, i, 0));
            outerCells.Add(new Vector3Int(11, i, 0));
            outerCells.Add(new Vector3Int(12, i, 0));
        }
        outerCells.Add(new Vector3Int(9, -5, 0));
        outerCells.Add(new Vector3Int(10, -5, 0));
        outerCells.Add(new Vector3Int(11, -5, 0));
        outerCells.Add(new Vector3Int(12, -5, 0));
        outerCells.Add(new Vector3Int(9, -6, 0));
        outerCells.Add(new Vector3Int(10, -6, 0));
        outerCells.Add(new Vector3Int(11, -6, 0));
        outerCells.Add(new Vector3Int(12, -6, 0));
        outerCells.Add(new Vector3Int(13, 5, 0));
        outerCells.Add(new Vector3Int(13, -6, 0));

        for (int i = 0; i < outerCells.Count; i++)
        {
            outerCells[i] += new Vector3Int(-1, -1, 0);
        }
    }

    private void FillDoors()
    {
        doors.Add(new Vector3Int(-7, 9, 0));
        doors.Add(new Vector3Int(-6, 4, 0));
        doors.Add(new Vector3Int(-9, 2, 0));
        doors.Add(new Vector3Int(-11, -1, 0));
        doors.Add(new Vector3Int(-7, -4, 0));
        doors.Add(new Vector3Int(-8, -8, 0));
        doors.Add(new Vector3Int(-3, 8, 0));
        doors.Add(new Vector3Int(-1, 6, 0));
        doors.Add(new Vector3Int(1, 6, 0));
        doors.Add(new Vector3Int(-4, -8, 0));
        doors.Add(new Vector3Int(-3, -6, 0));
        doors.Add(new Vector3Int(3, -6, 0));
        doors.Add(new Vector3Int(4, -8, 0));
        doors.Add(new Vector3Int(9, -7, 0));
        doors.Add(new Vector3Int(7, 3, 0));
        doors.Add(new Vector3Int(6, 7, 0));
    }

    private void FillRooms()
    {
        rooms.Add("Study", new Vector3Int(-9, 10, 0));
        rooms.Add("Library", new Vector3Int(-9, 4, 0));
        rooms.Add("Billiard Room", new Vector3Int(-9, -3, 0));
        rooms.Add("Conservatory", new Vector3Int(-9, -10, 0));
        rooms.Add("Hall", new Vector3Int(1, 8, 0));
        rooms.Add("Ball Room", new Vector3Int(1, -9, 0));
        rooms.Add("Kitchen", new Vector3Int(10, -10, 0));
        rooms.Add("Dining Room", new Vector3Int(10, 1, 0));
        rooms.Add("Lounge", new Vector3Int(10, 10, 0));
    }

    private void FillRoomsToDoors()
    {
        roomsToDoors.Add("Study", new List<Vector3Int> { doors[0] });
        roomsToDoors.Add("Library", new List<Vector3Int> { doors[1], doors[2] });
        roomsToDoors.Add("Billiard Room", new List<Vector3Int> { doors[3], doors[4] });
        roomsToDoors.Add("Conservatory", new List<Vector3Int> { doors[5] });
        roomsToDoors.Add("Hall", new List<Vector3Int> { doors[6], doors[7], doors[8] });
        roomsToDoors.Add("Ball Room", new List<Vector3Int> { doors[9], doors[10], doors[11], doors[12] });
        roomsToDoors.Add("Kitchen", new List<Vector3Int> { doors[13] });
        roomsToDoors.Add("Dining Room", new List<Vector3Int> { doors[14] });
        roomsToDoors.Add("Lounge", new List<Vector3Int> { doors[15] });
    }

    public Action PlayerRolledTheDice;
    public Action PlayerFinishedTurn;
    public Action PlayerFinishedMove;
    public Desicion PlayerMadeAccuse;

    private PhotonView pView;
    private GameObject player;
    private Vector3Int currentPosition;
    private bool canMove;

    private int dice;

    private List<Card> cards;
    private Card chosenCard;
    private GameObject[] version = new GameObject[3];

    int id, currentId;

    private GameObject shownCard;

    CyclicPlayersQueue playersQueue;
    SinglePlayersQueue showCardQueue;

    public string cellType;
    public bool CanMove
    {
        set
        {
            canMove = value;
        }
    }

    public Vector3Int CurrentPosition
    {
        get
        {
            return currentPosition;
        }
    }

    public int Dice
    {
        get
        {
            return dice;
        }
    }

    public string CellType
    {
        get
        {
            return cellType;
        }
    }

    private bool IsMyTurn
    {
        get
        {
            return id == currentId;
        }
    }
    public void Awake()
    {
        id = PhotonNetwork.LocalPlayer.ActorNumber;
        InitializeCellBoardPositions();
        InitializePlayersNames();

        cellType = "outer";
        nicknames = GetNames();

        CreatePlayerOnBoard();
        pView = player.GetComponent<PhotonView>();
        CreateAvatars();
        playersQueue = new CyclicPlayersQueue(0, SetTurn);
    }

    private void CreatePlayerOnBoard()
    {
        if (id < 7)
        {
            player = PhotonNetwork.Instantiate(playersNames[id - 1], new Vector3(), Quaternion.identity, 0);
            Move(cellBoardPositions[id - 1]);
        }
        canMove = false;
    }

    private void CreateAvatars()
    {
        uiManager.SetAvatars(id, GetNames());
    }

    #region Setters
    public void SetCards(int[] order)
    {
        int playerCount = 3;
        int playerId = id - 1;
        int countForPlayer = 18 / playerCount;
        int start = playerId * countForPlayer;
        int end = start + countForPlayer;
        cards = new List<Card>();
        for (int i = start; i < end; i++)
        {
            cardCollection[order[i]].transform.SetParent(uiManager.GetCardPanel(), false);
            cards.Add(cardCollection[order[i]].GetComponent<Card>());
            cards[i - start].CardChosen += SetChosenCard;
        }

        Card[] left = null;
        if (playerCount == 4)
        {
            left = new Card[] { cardCollection[order[16]].GetComponent<Card>(), 
                cardCollection[order[17]].GetComponent<Card>() };
        }
        else if (playerCount == 5)
        {
            left = new Card[] { cardCollection[order[15]].GetComponent<Card>(),
                cardCollection[order[16]].GetComponent<Card>(),
                cardCollection[order[17]].GetComponent<Card>()
            };
        }

        table.SetLeftCards(left);

        playersQueue.Next();
    }

    public void SetChosenCard(Card card)
    {
        if (chosenCard == null)
        {
            chosenCard = card;
            card.transform.position += new Vector3(0, 50, 0);
        }
        else if (chosenCard.GetId() == card.GetId())
        {
            chosenCard = null;
            card.transform.position -= new Vector3(0, 50, 0);
            return;
        }
        else if (chosenCard.GetId() != card.GetId())
        {
            cardCollection[chosenCard.GetId()].transform.position -= new Vector3(0, 50, 0);
            chosenCard = card;
            card.transform.position += new Vector3(0, 50, 0);
        }
    }

    public void SetTurn(int id)
    {
        if (id == -1)
        {
            uiManager.OnGameFinished(cardCollection.GetRightVersion());
            return;
        }
        currentId = id;
        showCardQueue = new SinglePlayersQueue(currentId, OnShowCardQueueSwithed);
        uiManager.OnSetTurn(currentId);
        if (IsMyTurn)
        {
            uiManager.OnMyTurn();
        }
        else
        {
            uiManager.OnOtherTurn(nicknames[id - 1]);
        }
    }

    public void SetVersion(int[] version)
    {
        if (version.Length == 0)
        {
            Debug.Log("no version");
            OnPlayerFinishedTurn();
            return;
        }
        this.version[0] = cardCollection[version[0]];
        this.version[1] = cardCollection[version[1]];
        this.version[2] = cardCollection[version[2]];

        uiManager.OnVersionSet(currentId, this.version);

        showCardQueue.Next();
    }

    public void SetShownCard(int id)
    {
        Debug.Log("set shown card");
        if (id == -1)
        {
            StartCoroutine(kek());
        }
        else
        {
            shownCard = cardCollection[id];
            StartCoroutine(kek2());
        }
    }

    IEnumerator kek()
    {
        uiManager.OnHaveNoCardResponse(showCardQueue.Current);
        yield return new WaitForSeconds(3);
        if (!showCardQueue.Next())
          {
              OnPlayerFinishedTurn();
          }
    }

    IEnumerator kek2()
    {
        uiManager.OnHaveCardResponse(playersQueue.Current, showCardQueue.Current, shownCard);
        yield return new WaitForSeconds(3);
        OnPlayerFinishedTurn();
    }

    private void OnPlayerFinishedTurn()
    {
        print("OnPlayerFinishedTurn");
        uiManager.OnPlayerFinishedTurn();
        playersQueue.Next();
    }

    public void SetAccuseVersion(int[] version)
    {
        this.version[0] = cardCollection[version[0]];
        this.version[1] = cardCollection[version[1]];
        this.version[2] = cardCollection[version[2]];

        uiManager.OnVersionSet(currentId, this.version);
        bool isRight = CheckVersion(version);

        if (isRight)
        {
            StartCoroutine(AccuseRightEnumerator());
        }
        else
        {
            StartCoroutine(AccuseWrongEnumerator());
        }
    }

    IEnumerator AccuseWrongEnumerator()
    {
        GameObject[] rightVersion = cardCollection.GetRightVersion();
        playersQueue.SetInactive(currentId);
        uiManager.OnPlayerLoose(PhotonNetwork.CurrentRoom.Players[currentId].NickName);
        if (IsMyTurn)
        {
            table.SetRightVersion(rightVersion);
        }
        yield return new WaitForSeconds(2);
        OnPlayerFinishedTurn();
    }

    IEnumerator AccuseRightEnumerator() {
        GameObject[] rightVersion = cardCollection.GetRightVersion();
        uiManager.OnPlayerWon(currentId, rightVersion);
        yield return new WaitForSeconds(3);
    }

    private bool CheckVersion(int[] version)
    {
        return cardCollection.IsAccuseRight(version);
    }

    #endregion

    #region Input Listeners

    public void RollTheDice()
    {
        Debug.Log("roll the dice");
        uiManager.OnDiceButtonClick();
        dice = UnityEngine.Random.Range(2, 13);
        Debug.Log("dice = " + dice);
        PlayerRolledTheDice?.Invoke();
        canMove = true;
    }

    public void GoOnCell(Vector3Int cell, string cellType)
    {
        if (canMove)
        {
            Debug.Log("go on " + cellType + " cell");
            this.cellType = cellType;
            if (pView.IsMine)
            {
                Move(cell);
            }
            if (cellType.Contains("outer"))
            {
                pView.RPC("MakeVersion_RPC", RpcTarget.All, new int[] { });
            }
            else
            {
                uiManager.OnMakeSuggestion();
            }
        }
    }

    public void MakeSuggestion()
    {
        int[] version;
        try
        {
            version = table.GetVersion();
        }
        catch (ArgumentException)
        {
            Debug.Log("version id not valid");
            return;
        }
        print(cardCollection[version[2]].GetComponent<Card>().GetName());
        print(cellType);
        if (!cellType.Contains(cardCollection[version[2]].GetComponent<Card>().GetName()))
        {
            Debug.Log("version is not valid");
            return;
        }
        pView.RPC("MakeVersion_RPC", RpcTarget.All, version);
    }

    public void Answer()
    {
        if (chosenCard == null &&
            (cards.Contains(version[0].GetComponent<Card>()) ||
            cards.Contains(version[1].GetComponent<Card>()) ||
            cards.Contains(version[2].GetComponent<Card>())))
        {
            return;
        }
        if (chosenCard == null)
        {
            pView.RPC("ShowCard_RPC", RpcTarget.All, -1);
            return;
        }
        if (version[0].GetComponent<Card>().GetId() != chosenCard.GetId() &&
            version[1].GetComponent<Card>().GetId() != chosenCard.GetId() &&
            version[2].GetComponent<Card>().GetId() != chosenCard.GetId())
        {
            return;
        }
        pView.RPC("ShowCard_RPC", RpcTarget.All, chosenCard.GetComponent<Card>().GetId());
    }

    public void GoThroughPassage()
    {
        canMove = true;
        uiManager.OnPassageButtonClick();
    }

    public void MakeAccusation()
    {
        try
        {
            pView.RPC("Accuse_RPC", RpcTarget.All, table.GetVersion());
        }
        catch (ArgumentException)
        {
            Debug.Log("version is not valid");
        }
    }

    public void SwitchTable()
    {
        uiManager.SwitchTable();
    }

    public void OnExitButtonClick()
    {
        PhotonNetwork.LeaveRoom();
    }

    #endregion

    #region Utils
    private void Move(Vector3Int cell)
    {
        player.transform.position = cell + new Vector3(0.5f, 0.5f, 0);
        currentPosition = cell;
        PlayerFinishedMove?.Invoke();
        canMove = false;
    }

    private void OnShowCardQueueSwithed()
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == showCardQueue.Current)
        {
            foreach (Card card in cards)
            {
                if (version[0].GetComponent<Card>().GetId() == card.GetId() ||
                    version[1].GetComponent<Card>().GetId() == card.GetId() ||
                    version[2].GetComponent<Card>().GetId() == card.GetId())
                {
                    card.gameObject.SetActive(true);
                }
            }
        }
        uiManager.OnShowCard(showCardQueue.Current);
    }

    #endregion

    #region Что это вообще здесь делает
    private string[] GetNames()
    {
        string[] res = new string[PhotonNetwork.CurrentRoom.PlayerCount];
        for (int i = 1; i <= res.Length; i++)
        {
            res[i - 1] = PhotonNetwork.CurrentRoom.Players[i].NickName;
        }
        return res;
    }

    private Vector3Int[] cellBoardPositions;
    private string[] playersNames;

    private void InitializePlayersNames()
    {
        playersNames = new string[6];
        playersNames[0] = "RedCapsule";
        playersNames[1] = "YellowCapsule";
        playersNames[2] = "GreenCapsule";
        playersNames[3] = "WhiteCapsule";
        playersNames[4] = "BlueCapsule";
        playersNames[5] = "PurpleCapsule";
    }

    private void InitializeCellBoardPositions()
    {
        cellBoardPositions = new Vector3Int[6];
        cellBoardPositions[0] = new Vector3Int(4, 11, 0);
        cellBoardPositions[1] = new Vector3Int(12, 4, 0);
        cellBoardPositions[2] = new Vector3Int(12, -6, 0);
        cellBoardPositions[3] = new Vector3Int(2, -13, 0);
        cellBoardPositions[4] = new Vector3Int(-2, -13, 0);
        cellBoardPositions[5] = new Vector3Int(-12, -7, 0);
    }
    #endregion
}
