using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public static bool IsGameStarted = false;
    public GameObject Logo, Start, Count, Lose;

    private bool IsLoseGame = false;

    public void PlayGame()
    {
        if (!IsLoseGame)
        {
            IsGameStarted = true;
            Logo.SetActive(false);
            Start.SetActive(false);
            Count.SetActive(true);
            Lose.SetActive(false);
        }
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoseGame()
    {
        IsLoseGame = true;
        IsGameStarted = false;
        Logo.SetActive(true);
        Start.SetActive(true);
        Count.SetActive(false);
        Lose.SetActive(true);
    }
}
