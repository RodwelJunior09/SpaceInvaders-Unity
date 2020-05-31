using TMPro;
using UnityEngine;

public class DisplayScore : MonoBehaviour
{
    private TextMeshProUGUI scoreTextDisplay;
    private GameStatus gameStatus;

    // Start is called before the first frame update
    void Start()
    {
        gameStatus = FindObjectOfType<GameStatus>();
        scoreTextDisplay = GetComponent<TextMeshProUGUI>();
        scoreTextDisplay.text = gameStatus.GetScore().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        scoreTextDisplay.text = gameStatus.GetScore().ToString();
    }
}
