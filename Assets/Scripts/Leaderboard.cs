using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    private GameStatus _gameStatus;
    private InputField _nameField;

    [SerializeField] TextMeshProUGUI _playerNameTextField;
    [SerializeField] TextMeshProUGUI _playerScoreTextField;
    [SerializeField] TextMeshProUGUI _playerDateTextField;

    // Start is called before the first frame update
    void Start()
    {
        _gameStatus = FindObjectOfType<GameStatus>();
        _nameField = FindObjectOfType<InputField>();
        _playerDateTextField.text = DateTime.Now.ToString("MMM d, yyyy");
        _playerScoreTextField.text = PlayerPrefs.GetInt("maxScore", 999).ToString();
        _playerNameTextField.text = PlayerPrefs.GetString("playername", "PlayerName");
    }

    public void GetPlayerInfo(){
        PlayerPrefs.SetString("playername", _nameField.text);
        PlayerPrefs.SetInt("maxScore", _gameStatus.GetScore());
        PlayerPrefs.SetString("date", DateTime.Now.ToString("MMM d, yyyy"));

        _playerNameTextField.text = PlayerPrefs.GetString("playername");
        _playerScoreTextField.text = PlayerPrefs.GetInt("maxScore").ToString();
        _playerDateTextField.text = PlayerPrefs.GetString("date");
    }
}
