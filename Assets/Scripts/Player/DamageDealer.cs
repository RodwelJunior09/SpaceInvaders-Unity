using UnityEngine;

namespace Player
{
    public class DamageDealer : MonoBehaviour
    {
        [SerializeField] private int playerDamage = 100;
        [SerializeField] private int enemyDamage = 100;

        private bool _isFromEnemy;

        private void Awake()
        {
            // Enemy ships and their projectiles live on the Enemy / EnemyLaser layers
            var layer = gameObject.layer;
            _isFromEnemy = layer == LayerMask.NameToLayer("Enemy") || layer == LayerMask.NameToLayer("EnemyLaser");
        }

        public int GetDamage()
        {
            return _isFromEnemy ? enemyDamage : playerDamage;
        }

        public void Hit()
        {
            // An enemy ship that rams the player should die properly (explosion and score), not just vanish
            var enemy = GetComponent<Enemy>();
            if (enemy) enemy.Kill();
            else Destroy(gameObject);
        }
    }
}
