using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    private const string DateFormat = "MMM d, yyyy";
    private const string DefaultPlayerName = "PlayerName";
    private const int MaxNameLength = 16;

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
        if (_nameField) _nameField.characterLimit = MaxNameLength;
        // The name is typed by the player, so don't let it inject TextMeshPro rich text tags
        _playerNameTextField.richText = false;
        ShowSavedRecord();
    }

    public void GetPlayerInfo(){
        int score = _gameStatus ? _gameStatus.GetScore() : 0;
        // Only replace the record when it has been beaten
        if (score > PlayerPrefs.GetInt("maxScore", 0))
        {
            PlayerPrefs.SetString("playername", GetEnteredName());
            PlayerPrefs.SetInt("maxScore", score);
            PlayerPrefs.SetString("date", DateTime.Now.ToString(DateFormat));
            PlayerPrefs.Save();
        }
        ShowSavedRecord();
    }

    private string GetEnteredName()
    {
        var enteredName = _nameField ? _nameField.text.Trim() : string.Empty;
        if (enteredName.Length == 0) return DefaultPlayerName;
        return enteredName.Length > MaxNameLength ? enteredName.Substring(0, MaxNameLength) : enteredName;
    }

    private void ShowSavedRecord()
    {
        _playerNameTextField.text = PlayerPrefs.GetString("playername", DefaultPlayerName);
        _playerScoreTextField.text = PlayerPrefs.GetInt("maxScore", 0).ToString();
        _playerDateTextField.text = PlayerPrefs.GetString("date", DateTime.Now.ToString(DateFormat));
    }
}
