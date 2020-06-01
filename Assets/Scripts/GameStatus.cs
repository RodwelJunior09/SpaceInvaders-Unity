using UnityEngine;

public class GameStatus : MonoBehaviour
{
    private int gameScore = 0;

    void Awake()
    {
        SaveData();
    }

    public void AddToScore(int pointPerEnemy)
    {
        gameScore += pointPerEnemy;
    }

    public int GetScore()
    {
        return gameScore;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    private void SaveData()
    {
        int gamesObjects = FindObjectsOfType<MusicPlayer>().Length;
        if (gamesObjects > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
