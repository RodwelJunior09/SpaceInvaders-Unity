using TMPro;
using UnityEngine;

public class DisplayScore : MonoBehaviour
{
    private GameStatus _gameStatus;
    private TextMeshProUGUI _scoreTextDisplay;

    // Start is called before the first frame update
    void Start()
    {
        _gameStatus = FindObjectOfType<GameStatus>();
        _scoreTextDisplay = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        _scoreTextDisplay.text = _gameStatus.GetScore().ToString();
    }
}
