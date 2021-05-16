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

    [SerializeField] private Transform notifyPanel;
    [SerializeField] private Text notifyText;

    [SerializeField] private Transform versionPanel;

    [SerializeField] private Image[] dice;

    public void Awake()
    {
        Debug.Log("ui awake");
        CloseTable();
        for (int i = 0; i < dice.Length; i++)
        {
            dice[i].DOFade(0, 0);
        }
    }

    public void Start()
    {
        Debug.Log("ui start " + PhotonNetwork.LocalPlayer.ActorNumber);
    }

    public void SetLeftCards(Card[] left)
    {
        if (left == null)
        {
            return;
        }
        Toggle[] toggles = tablePanel.GetComponentsInChildren<Toggle>();
        toggles[left[0].GetId()].isOn = true;
        toggles[left[0].GetId()].interactable = false;
        toggles[left[1].GetId()].isOn = true;
        toggles[left[1].GetId()].interactable = false;
        toggles[left[2].GetId()].isOn = true;
        toggles[left[2].GetId()].interactable = false;
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

    #endregion

    public void OnMyTurn()
    {
        Debug.Log("ui on my turn");
        //Block();
        diceButton.interactable = true;
        notifyText.text = "This is your turn";
    }

    public void OnDiceButtonClick()
    {
        Debug.Log("ui on dice button click");
        notifyText.text = "";
        //diceButton.interactable = false;
        for (int i = 0; i < dice.Length; i++) {
            StartCoroutine(DiceTween(i));
        }
    }

    IEnumerator DiceTween(int i)
    {
        Sequence tween = DOTween.Sequence().Append(dice[i].DOFade(1, 0.5f))
            .Append(dice[i].DOFade(0, 0.5f));
        yield return tween.WaitForCompletion();
        print("tween completed");
    }

    public void OnMakeSuggestion()
    {
        Debug.Log("on move release");
        versionButton.interactable = true;
        tryButton.interactable = true;
        ShowTable();
    }

    public void OnVersionSet(int currentId, GameObject[] version)
    {
        Debug.Log("ui on version set");
        CloseTable();
        ShowVersion(currentId, version);
    }

    public void OnOtherTurn()
    {
        Debug.Log("ui on other turn");
        Block();
    }

    public void OnShowCard(int id)
    {
        Debug.Log("ui on show card");
        UnblockShowCard(id);
    }

    public void OnHaveCardResponse(int id, int showId, GameObject card)
    {
        ShowCard(id, showId, card);
    }

    public void OnHaveNoCardResponse(int id)
    {
        BlockShowCard(id);
        SayNoCards(id);
    }

    private void SayNoCards(int id)
    {
        avatars[id - 1].Say("I don't have this card.");
    }

    public void OnPlayerWon(int id, GameObject[] version)
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == id)
        {
            notifyText.text = "you won";
        }
        else
        {
            notifyText.text = "player " + id + " won";
        }
    }

    public void OnPlayerLoose(int id, GameObject[] version)
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == id)
        {
            notifyText.text = "you loose";
        }
        else
        {
            notifyText.text = "player " + id + " loose";
        }
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
        GameObject character = Instantiate(version[0], new Vector3(), Quaternion.identity);
        character.transform.SetParent(versionPanel);
        GameObject tool = Instantiate(version[1], new Vector3(), Quaternion.identity);
        tool.transform.SetParent(versionPanel);
        GameObject room = Instantiate(version[2], new Vector3(), Quaternion.identity);
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
        GameObject c = Instantiate(card, notifyPanel);
        c.GetComponentInChildren<Image>().transform.DOScale(new Vector3(2, 2, 2), 3);
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

    private void SayNothing(int id)
    {
        Debug.Log("SayNothing");
        avatars[id - 1].Say("");
    }

    private void SayHaveCard(int id)
    {
        avatars[id - 1].Say("I have the card");
    }

    public void Remove()
    {
        notifyPanel.DetachChildren();
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
        Debug.Log("ui unblock show card " + id + " " + PhotonNetwork.LocalPlayer.ActorNumber);
        if (PhotonNetwork.LocalPlayer.ActorNumber == id)
        {
            showButton.interactable = true;
        }
    }

    private void ShowResponse(int id, bool response)
    {
        Debug.Log("show reponse");
        if (response)
        {
            avatars[id - 1].Say("I have this card");
        }
        else
        {
            avatars[id - 1].Say("I don't have this card");
        }
    }
}
