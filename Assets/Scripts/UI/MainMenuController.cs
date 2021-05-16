using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void OnSingleplayerButtonClick()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnMultiplayerButtonClick()
    {
        SceneManager.LoadScene("MultiplayerMenu");
    }

    public void OnSettingsButtonClick()
    {
        SceneManager.LoadScene("Settings");
    }

    public void OnExitButton()
    {
        Application.Quit();
    }
}
