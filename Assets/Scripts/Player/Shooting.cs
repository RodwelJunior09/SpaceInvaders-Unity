using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class Shooting : MonoBehaviour
    {
        [FormerlySerializedAs("_playerLaser")]
        [Header("Projectile Config")]
        [SerializeField] private GameObject playerLaser;
        [SerializeField] private float projectileSpeed = 1f;
        [SerializeField] private float projectFirePeriod = 0.25f;

        [FormerlySerializedAs("_laserAudioClips")] [SerializeField] private AudioClip laserAudioClips;
        [FormerlySerializedAs("_laserVolume")] [SerializeField, Range(0f, 1f)] private float laserVolume = 1f;

        private int _attackBonus;

        // Applied to each spawned laser so the prefab asset is never modified
        public void IncreaseAttack(int increaseAmount)
        {
            _attackBonus += increaseAmount;
            projectileSpeed += 4;
            if (projectFirePeriod > 0.1f)
                projectFirePeriod -= 0.05f;
        }

        private IEnumerator ContinuousFire()
        {
            while (true)
            {
                var laser = Instantiate(playerLaser, transform.position, Quaternion.identity);
                laser.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, projectileSpeed);
                if (_attackBonus > 0) laser.GetComponent<DamageDealer>().IncreaseDamage(_attackBonus);
                if (Camera.main)
                    AudioSource.PlayClipAtPoint(laserAudioClips, Camera.main.transform.position, laserVolume);
                yield return new WaitForSeconds(projectFirePeriod);
            }
        }

        private void Start()
        {
            StartCoroutine(ContinuousFire());
        }
    }
}
