using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public void LoadStartMenuScene()
    {
        FindObjectOfType<GameStatus>().ResetGame();
        SceneManager.LoadScene("StartMenu");
    }

    public void LoadPlayerWins()
    {
        StartCoroutine(WinScene());
    }

    public void LoadGameOver()
    {
        StartCoroutine(GameOverScene());
    }

    private IEnumerator GameOverScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("GameOver");
    }

    private IEnumerator WinScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("WinScene");
    }

    public void LoadFirstLevel()
    {
        FindObjectOfType<GameStatus>().ResetGame();
        SceneManager.LoadScene("Lvl1");
    }

    public void LoadNextScene()
    {
        int currentIndexScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndexScene + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
