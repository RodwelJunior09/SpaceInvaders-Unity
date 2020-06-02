using TMPro;
using UnityEngine;

public class DisplayScore : MonoBehaviour
{
    private TextMeshProUGUI _scoreTextDisplay;
    private GameStatus _gameStatus;

    // Start is called before the first frame update
    void Start()
    {
        _scoreTextDisplay = GetComponent<TextMeshProUGUI>();
        _gameStatus = FindObjectOfType<GameStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        _scoreTextDisplay.text = _gameStatus.GetScore().ToString();
    }
}
