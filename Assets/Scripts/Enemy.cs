﻿using UnityEngine;

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

    [SerializeField] private float _bossFireLaserRotation = 1f;

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
        var laser = Instantiate(enemyLaserPrefab, transform.position, Quaternion.identity);
        if (tag.Contains("Boss")) laser = Instantiate(enemyLaserPrefab, transform.position, transform.rotation);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(_laserAudioClip, Camera.main.transform.position, _laserVolume);
    }

    private void OnTriggerEnter2D(Collider2D otherCollision)
    {
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
        FindObjectOfType<Level>().LoadPlayerWins();
    }

    private void Die()
    {
        var enemyObject = gameObject;
        Destroy(enemyObject);
        FindObjectOfType<GameStatus>().AddToScore(scoreOfEnemyKilled);
        GameObject vfxExplosion = Instantiate(explosionGameObject, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(_explosionAudioClip, Camera.main.transform.position, _explosionVolume);
        Destroy(vfxExplosion, durationExplosion);
        if (enemyObject.tag.Contains("Boss")) PlayerSuccess();
    }
}
