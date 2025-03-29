using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play1Player()
    {
        SceneManager.LoadScene("Tetris1Player");
    }
    public void Play2Player()
    {
        SceneManager.LoadScene("Tetris2Player");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
