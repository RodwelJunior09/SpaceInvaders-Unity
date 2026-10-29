using UnityEngine;

public class GameStatus : MonoBehaviour
{
    [SerializeField, Min(1)] private int _powerUpScoreInterval = 150;

    private PowerUp powerUp;
    private int gameScore = 0;
    private int _nextPowerUpScore;

    void Awake()
    {
        SaveData();
        _nextPowerUpScore = _powerUpScoreInterval;
    }

    public void AddToScore(int pointPerEnemy)
    {
        gameScore += pointPerEnemy;
        if (gameScore < _nextPowerUpScore) return;

        // A big kill can jump past several thresholds, only spawn one item for it
        while (_nextPowerUpScore <= gameScore) _nextPowerUpScore += _powerUpScoreInterval;

        // This object survives scene loads, so look the power up spawner up again when needed
        if (!powerUp) powerUp = FindFirstObjectByType<PowerUp>();
        if (powerUp) powerUp.ShowItemPowerUp();
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
        int gamesObjects = FindObjectsByType<GameStatus>(FindObjectsSortMode.None).Length;
        if (gamesObjects > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
