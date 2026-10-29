using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int playerDamage = 100;
    [SerializeField] private int enemyDamage = 100;

    private bool _isFromEnemy;

    void Awake()
    {
        // Enemy ships and their projectiles live on the Enemy / EnemyLaser layers
        int layer = gameObject.layer;
        _isFromEnemy = layer == LayerMask.NameToLayer("Enemy") || layer == LayerMask.NameToLayer("EnemyLaser");
    }

    public int GetDamage()
    {
        return _isFromEnemy ? enemyDamage : playerDamage;
    }

    // Only call this on spawned instances, never on the prefab asset
    public void IncreaseDamage(int increaseAmount)
    {
        playerDamage += increaseAmount;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
