using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUpItem : MonoBehaviour
{
    // Same order as the sprites in _allPowerUpsSprites
    static readonly string[] PowerUpNames = { "health", "defense", "attack" };

    [SerializeField] Sprite[] _allPowerUpsSprites;

    SpriteRenderer item_viewer;

    string powerUpName;

    // Picked in Awake so the item always has a type before any trigger can fire
    void Awake(){
        item_viewer = GetComponentInChildren<SpriteRenderer>();
        int index = Random.Range(0, PowerUpNames.Length);
        powerUpName = PowerUpNames[index];
        item_viewer.sprite = _allPowerUpsSprites[index];
    }

    private void OnTriggerEnter2D(Collider2D collision){
        var player = collision.GetComponent<Player>();
        if (player)
        {
            player.BuffsToPlayer(powerUpName);
            Destroy(gameObject);
        }
    }
}
