using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    private const string DateFormat = "MMM d, yyyy";

    private GameStatus _gameStatus;
    private InputField _nameField;

    [SerializeField] TextMeshProUGUI _playerNameTextField;
    [SerializeField] TextMeshProUGUI _playerScoreTextField;
    [SerializeField] TextMeshProUGUI _playerDateTextField;

    // Start is called before the first frame update
    void Start()
    {
        _gameStatus = FindFirstObjectByType<GameStatus>();
        _nameField = FindFirstObjectByType<InputField>();
        ShowSavedRecord();
    }

    public void GetPlayerInfo(){
        int score = _gameStatus ? _gameStatus.GetScore() : 0;
        // Only replace the record when it has been beaten
        if (score > PlayerPrefs.GetInt("maxScore", 0))
        {
            PlayerPrefs.SetString("playername", _nameField.text);
            PlayerPrefs.SetInt("maxScore", score);
            PlayerPrefs.SetString("date", DateTime.Now.ToString(DateFormat));
            PlayerPrefs.Save();
        }
        ShowSavedRecord();
    }

    private void ShowSavedRecord()
    {
        _playerNameTextField.text = PlayerPrefs.GetString("playername", "PlayerName");
        _playerScoreTextField.text = PlayerPrefs.GetInt("maxScore", 0).ToString();
        _playerDateTextField.text = PlayerPrefs.GetString("date", DateTime.Now.ToString(DateFormat));
    }
}
