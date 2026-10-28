using UnityEngine;

public class GameStatus : MonoBehaviour
{
    PowerUp powerUp;
    private int gameScore = 0;

    // Coool
    void Awake()
    {
        SaveData(); // Hello world...
    }

    void Start(){
        powerUp = FindObjectOfType<PowerUp>();
    }

    public void AddToScore(int pointPerEnemy)
    {
        gameScore += pointPerEnemy;
        if (GetScore() % 150 == 0)
            powerUp.ShowItemPowerUp();
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
        int gamesObjects = FindObjectsOfType<GameStatus>().Length;
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
