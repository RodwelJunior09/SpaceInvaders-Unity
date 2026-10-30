using UnityEngine;
using Random = UnityEngine.Random;

namespace UI
{
    public class PowerUpItem : MonoBehaviour
    {
        // Same order as the sprites in _allPowerUpsSprites
        private static readonly string[] PowerUpNames = { "health", "defense", "attack" };

        [SerializeField] Sprite[] _allPowerUpsSprites;

        private SpriteRenderer _itemViewer;

        private string _powerUpName;

        // Picked in Awake so the item always has a type before any trigger can fire
        private void Awake(){
            _itemViewer = GetComponentInChildren<SpriteRenderer>();
            var index = Random.Range(0, PowerUpNames.Length);
            _powerUpName = PowerUpNames[index];
            _itemViewer.sprite = _allPowerUpsSprites[index];
        }

        private void OnTriggerEnter2D(Collider2D collision){
            var player = collision.GetComponent<Player.Player>();
            if (!player) return;
            player.BuffsToPlayer(_powerUpName);
            Destroy(gameObject);
        }
    }
}
