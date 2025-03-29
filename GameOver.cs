using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject gameOver;
    public void Over()
    {
        gameOver.SetActive(true);
        Debug.Log("działa koniec gry");
    }
}
