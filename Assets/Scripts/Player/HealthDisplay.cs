using TMPro;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    private TextMeshProUGUI healthTextDisplay;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        healthTextDisplay = GetComponent<TextMeshProUGUI>();
        healthTextDisplay.text = player.GetPlayerHealth().ToString();
    }
    // Cool
    void UpdatePlayerHealth()
    {
        var playerHealth = player.GetPlayerHealth();
        if (playerHealth < 0)
        {
            healthTextDisplay.text = "0";
        }
        else
        {
            healthTextDisplay.text = playerHealth.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerHealth();
    }
}
