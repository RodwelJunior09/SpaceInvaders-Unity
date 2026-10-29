using TMPro;
using UnityEngine;

public class DisplayScore : MonoBehaviour
{
    private GameStatus _gameStatus;
    private TextMeshProUGUI _scoreTextDisplay;
    private int _shownScore = int.MinValue;

    // Start is called before the first frame update
    void Start()
    {
        _gameStatus = FindFirstObjectByType<GameStatus>();
        _scoreTextDisplay = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        int score = _gameStatus ? _gameStatus.GetScore() : 0;
        if (score == _shownScore) return;

        _shownScore = score;
        _scoreTextDisplay.text = score.ToString();
    }
}
