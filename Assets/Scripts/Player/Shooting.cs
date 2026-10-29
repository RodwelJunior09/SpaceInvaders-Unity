using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{
    [Header("Projectile Config")]
    [SerializeField] private GameObject _playerLaser;
    [SerializeField] private float projectileSpeed = 1f;
    [SerializeField] private float projectFirePeriod = 0.25f;

    [SerializeField] AudioClip _laserAudioClips;
    [SerializeField, Range(0f, 1f)] private float _laserVolume = 1f;

    private int _attackBonus;

    // Applied to each spawned laser so the prefab asset is never modified
    public void IncreaseAttack(int increaseAmount)
    {
        _attackBonus += increaseAmount;
    }

    private IEnumerator ContinousFire()
    {
        while (true)
        {
            var laser = Instantiate(_playerLaser, transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, projectileSpeed);
            if (_attackBonus > 0) laser.GetComponent<DamageDealer>().IncreaseDamage(_attackBonus);
            AudioSource.PlayClipAtPoint(_laserAudioClips, Camera.main.transform.position, _laserVolume);
            yield return new WaitForSeconds(projectFirePeriod);
        }
    }

    private void Start()
    {
        StartCoroutine(ContinousFire());
    }
}
