using TMPro;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    private TextMeshProUGUI healthTextDisplay;
    private Player.Player player;
    private int _shownHealth = int.MinValue;

    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<Player.Player>();
        healthTextDisplay = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // The player object is destroyed when it dies
        int playerHealth = player ? Mathf.Max(0, player.GetPlayerHealth()) : 0;
        if (playerHealth == _shownHealth) return;

        _shownHealth = playerHealth;
        healthTextDisplay.text = playerHealth.ToString();
    }
}
