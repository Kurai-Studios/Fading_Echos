using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Game Quited");
        //Application.Quit();
    }

    public void H2P()
    {
        SceneManager.LoadScene("How2Play");
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
