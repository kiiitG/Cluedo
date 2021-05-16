using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenuController : MonoBehaviour
{
    public void OnExitButtonClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
