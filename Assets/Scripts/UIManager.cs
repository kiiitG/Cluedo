using Photon.Pun;
using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Sprite[] pictures;
    private PlayerAvatar[] avatars;

    [SerializeField] private Canvas tablePanel;

    [SerializeField] private Button diceButton;
    [SerializeField] private Button showButton;
    [SerializeField] private Button secretButton;
    [SerializeField] private Button versionButton;
    [SerializeField] private Button tryButton;

    [SerializeField] private Transform opponentPanel;

    [SerializeField] private Canvas playerPanel;
    [SerializeField] private Transform avatarPanel;
    [SerializeField] private Canvas cardPanel;

    [SerializeField] private Text notifyText;

    [SerializeField] private Transform versionPanel;

    [SerializeField] private Image[] dice;

    public void Awake()
    {
        CloseTable();
        for (int i = 0; i < dice.Length; i++)
        {
            dice[i].DOFade(0, 0);
        }
    }

    #region Setters

    public void SetAvatars(int id, string[] nicknames)
    {
        avatars = new PlayerAvatar[nicknames.Length];
        for (int i = 0; i < nicknames.Length; i++)
        {
            Debug.Log(nicknames[i]);
            if (id - 1 == i)
            {
                avatars[i] = Instantiate(prefab, avatarPanel).GetComponent<PlayerAvatar>();
                avatars[i].SetPicture(pictures[i]);
            }
            else
            {
                avatars[i] = Instantiate(prefab, opponentPanel).GetComponent<PlayerAvatar>();
                avatars[i].SetPicture(pictures[i]);
            }
            avatars[i].SetNickname(nicknames[i]);
        }
    }

    public void OnSetTurn(int id)
    {
        for (int i = 0; i < avatars.Length; i++)
        {
            if (i == id - 1)
            {
                avatars[i].OnSetTurn();
            }
            else
            {
                avatars[i].OnDropTurn();
            }
        }
    }

    #endregion

    public void OnMyTurn()
    {
        Block();
        diceButton.interactable = true;
        secretButton.interactable = true;
        Notify("This is your turn");
    }

    private void Notify(string text)
    {
        notifyText.text = text;
        StartCoroutine(notifyTween());
    }

    IEnumerator notifyTween()
    {
        Tween tween = notifyText.DOFade(1, 1);
        yield return tween.WaitForCompletion();
        tween = notifyText.DOFade(0, 1);
        yield return tween.WaitForCompletion();
    }

    public void OnDiceButtonClick()
    {
        diceButton.interactable = false;
        secretButton.interactable = false;
        for (int i = 0; i < dice.Length; i++) {
            StartCoroutine(DiceTween(i));
        }
    }

    public void OnPassageButtonClick()
    {
        diceButton.interactable = false;
        secretButton.interactable = false;
    }

    IEnumerator DiceTween(int i)
    {
        Sequence tween = DOTween.Sequence().Append(dice[i].DOFade(1, 0.5f))
            .Append(dice[i].DOFade(0, 0.5f));
        yield return tween.WaitForCompletion();
    }

    public void OnMakeSuggestion()
    {
        versionButton.interactable = true;
        tryButton.interactable = true;
        ShowTable();
    }

    public void OnVersionSet(int currentId, GameObject[] version)
    {
        CloseTable();
        ShowVersion(currentId, version);
        versionButton.interactable = false;
        tryButton.interactable = false;
    }

    public void OnOtherTurn(string nickname)
    {
        Notify("This is " + nickname + "'s turn");
        Block();
    }

    public void OnShowCard(int id)
    {
        UnblockShowCard(id);
    }

    public void OnHaveCardResponse(int id, int showId, GameObject card)
    {
        BlockShowCard(id);
        ShowCard(id, showId, card);
    }

    public void OnHaveNoCardResponse(int id)
    {
        BlockShowCard(id);
        SayNoCards(id);
    }

    private void SayNoCards(int id)
    {
        avatars[id - 1].Say("I don't have any card");
    }

    public void OnPlayerWon(int id, GameObject[] version)
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == id)
        {
            Notify("you won");
        }
        else
        {
            Notify("player " + id + " won");
        }
        versionPanel.DetachChildren();
        ShowVersion(id, version);
        Block();
    }

    public void OnPlayerLeftGame(string nickname)
    {
        Notify(nickname + " left the room");
        Block();
    }

    public void OnPlayerLoose(string nickname)
    {
        Notify("player " + nickname + " loose");
    }

    public Transform GetCardPanel()
    {
        return cardPanel.transform;
    }

    public void SwitchTable()
    {
        tablePanel.gameObject.SetActive(!tablePanel.gameObject.activeInHierarchy);
    }

    private void Block()
    {
        diceButton.interactable = false;
        versionButton.interactable = false;
        tryButton.interactable = false;
        secretButton.interactable = false;
        showButton.interactable = false;
    }

    private void CloseTable()
    {
        tablePanel.gameObject.SetActive(false);
    }

    private void ShowTable()
    {
        tablePanel.gameObject.SetActive(true);
    }

    private void ShowVersion(int id, GameObject[] version)
    {
        avatars[id - 1].Say("I think it's " + version[0].GetComponent<Card>().GetName() + " " + 
            version[1].GetComponent<Card>().GetName() + " " + 
            version[2].GetComponent<Card>().GetName());
        ShowCards(version);
    }

    private void ShowCards(GameObject[] cards)
    {
        GameObject character = Instantiate(cards[0], new Vector3(), Quaternion.identity);
        character.transform.SetParent(versionPanel);
        GameObject tool = Instantiate(cards[1], new Vector3(), Quaternion.identity);
        tool.transform.SetParent(versionPanel);
        GameObject room = Instantiate(cards[2], new Vector3(), Quaternion.identity);
        room.transform.SetParent(versionPanel);
    }

    private void ShowCard(int id, int showId, GameObject card)
    {
        SayHaveCard(showId);
        if (PhotonNetwork.LocalPlayer.ActorNumber == id)
        {
            ShowCard(card);
        } 
        else
        {
            DoNothing();
        }
    }

    private void DoNothing()
    {
        StartCoroutine(kek2());
    }

    public void ShowCard(GameObject card)
    {
        StartCoroutine(kek(card));
    }

    IEnumerator kek(GameObject card)
    {
        GameObject c = Instantiate(card, notifyText.transform);
        c.GetComponentInChildren<Image>().transform.DOScale(new Vector3(3, 3, 3), 3);
        yield return new WaitForSeconds(3);
        Destroy(c.gameObject);
    }

    IEnumerator kek2()
    { 
        yield return new WaitForSeconds(3);
    }

    public void OnPlayerFinishedTurn()
    {
        versionPanel.DetachChildren();
    }

    private void SayHaveCard(int id)
    {
        avatars[id - 1].Say("I have the card");
    }

    private void BlockShowCard(int id)
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == id)
        {
            showButton.interactable = false;
        }
    }

    private void UnblockShowCard(int id)
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == id)
        {
            showButton.interactable = true;
        }
    }
  
    public void OnGameFinished(GameObject[] answer) {
        Notify("Game is over");
        ShowCards(answer);
        Block();
    }

    public void OnDisconnected()
    {
        Notify("connection is failed");
        Block();
    }
}
