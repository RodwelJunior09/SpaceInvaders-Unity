using Player;
using UI;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private float health = 500;
    [SerializeField] private float shotCounter;
    [SerializeField] private float minBetweenShots = 0.2f;
    [SerializeField] private float maxBetweenShots = 3f;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float durationExplosion = 1f;
    [SerializeField] private int scoreOfEnemyKilled = 10;

    [Header("Enemy Object Components")]
    [SerializeField] private GameObject explosionGameObject;
    [SerializeField] private GameObject enemyLaserPrefab;

    [Header("Enemy Sounds")]
    [SerializeField] private AudioClip _laserAudioClip;
    [SerializeField] private AudioClip _explosionAudioClip;

    [Header("Volume Adjustment")]
    [SerializeField, Range(0f, 1f)] private float _laserVolume = 0.5f;
    [SerializeField, Range(0f, 1f)] private float _explosionVolume = 0.5f;

    private bool _isDead;

    // Start is called before the first frame update
    void Start()
    {
        ResetShotCounter();
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            ResetShotCounter();
        }
    }

    private void ResetShotCounter()
    {
        shotCounter = Random.Range(minBetweenShots, maxBetweenShots);
    }

    private void Fire()
    {
        var rotation = CompareTag("Boss") ? transform.rotation : Quaternion.identity;
        var laser = Instantiate(enemyLaserPrefab, transform.position, rotation);
        laser.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(_laserAudioClip, Camera.main.transform.position, _laserVolume);
    }

    private void OnTriggerEnter2D(Collider2D otherCollision)
    {
        if (_isDead) return;
        var damageDealer = otherCollision.GetComponent<DamageDealer>();
        if (!damageDealer)
        {
            return;
        }
        HitProcess(damageDealer);
    }

    private void HitProcess(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void PlayerSuccess()
    {
        FindFirstObjectByType<Level>().LoadPlayerWins();
    }

    // Used when the enemy is destroyed by something other than a projectile, like ramming the player
    public void Kill()
    {
        if (!_isDead) Die();
    }

    private void Die()
    {
        // Destroy is deferred, so block a second hit in the same frame from killing us twice
        _isDead = true;
        Destroy(gameObject);
        var gameStatus = FindFirstObjectByType<GameStatus>();
        if (gameStatus) gameStatus.AddToScore(scoreOfEnemyKilled);
        GameObject vfxExplosion = Instantiate(explosionGameObject, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(_explosionAudioClip, Camera.main.transform.position, _explosionVolume);
        Destroy(vfxExplosion, durationExplosion);
        if (CompareTag("Boss")) PlayerSuccess();
    }
}
