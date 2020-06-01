using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public void LoadStartMenuScene()
    {
        SceneManager.LoadScene("StartMenu");
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
