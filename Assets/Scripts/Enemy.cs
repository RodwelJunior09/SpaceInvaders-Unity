using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float health = 800;
    [SerializeField] private float shotCounter;
    [SerializeField] private float minBetweenShots = 0.2f;
    [SerializeField] private float maxBetweenShots = 3f;

    [SerializeField] private GameObject explosionGameObject;
    [SerializeField] private GameObject enemyLaserPrefab;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float durationExplosion = 1f;

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
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
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

    private void Die()
    {
        Destroy(gameObject);
        GameObject vfxExplosion = Instantiate(explosionGameObject, transform.position, transform.rotation);
        Destroy(vfxExplosion, durationExplosion);
    }
}
