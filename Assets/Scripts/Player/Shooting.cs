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

        private const float MaxProjectileSpeed = 20f;
        private const float MinFirePeriod = 0.1f;

        // Clamped so float rounding or a high starting value can't push past the limits
        public void IncreaseSpeed()
        {
            projectileSpeed = Mathf.Min(projectileSpeed + 4f, MaxProjectileSpeed);
            projectFirePeriod = Mathf.Max(projectFirePeriod - 0.05f, MinFirePeriod);
        }

        private IEnumerator ContinuousFire()
        {
            while (true)
            {
                var laser = Instantiate(playerLaser, transform.position, Quaternion.identity);
                laser.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, projectileSpeed);
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
