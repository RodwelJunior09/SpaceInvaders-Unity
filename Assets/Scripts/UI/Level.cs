using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class Level : MonoBehaviour
    {
        public void LoadStartMenuScene()
        {
            ResetGameStatus();
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

        public void LoadLeaderBoard() => SceneManager.LoadScene("Leaderboard");

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
            ResetGameStatus();
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

        private void ResetGameStatus()
        {
            var gameStatus = FindFirstObjectByType<GameStatus>();
            if (gameStatus) gameStatus.ResetGame();
        }
    }
}
