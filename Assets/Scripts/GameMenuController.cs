using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuController : MonoBehaviour
{
    public void OnExitButtonClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}