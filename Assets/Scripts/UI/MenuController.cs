using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("Scene Dependencies")]
    [SerializeField] private NetworkManager networkManager;

    [Header("UI Elements")]
    [SerializeField] private GameObject UsernameMenu;
    [SerializeField] private GameObject ConnectPanel;
    [SerializeField] private InputField UsernameInputField;
    [SerializeField] private InputField JoinInputField;
    [SerializeField] private Button StartButton;
    [SerializeField] private Button JoinButton;

    public void SetUserName()
    {
        UsernameMenu.SetActive(false);
        networkManager.setUserName(UsernameInputField.text);
    }

    public void StartGame()
    {
        networkManager.setRoomName(JoinInputField.text);
        networkManager.Connect();
    }
}
