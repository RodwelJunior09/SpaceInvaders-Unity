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

    private Coroutine _fireCoroutine;

    public DamageDealer GetProjectile => _playerLaser.GetComponent<DamageDealer>();

    private IEnumerator ContinousFire()
    {
        while (true)
        {
            var laser = Instantiate(_playerLaser, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(_laserAudioClips, Camera.main.transform.position, _laserVolume);
            yield return new WaitForSeconds(projectFirePeriod);
        }
    }

    private void Start()
    {
        StartCoroutine(ContinousFire());
    }
}
