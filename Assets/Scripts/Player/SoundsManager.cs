using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [Header("Player Sounds")]
    [SerializeField] private AudioClip _bonusHealthAudioClip;
    [SerializeField, Range(0f, 1f)] private float _healthBonusVolume = 1f;

    [SerializeField] private AudioClip _damageAudioClip;
    [SerializeField, Range(0f, 1f)] private float _damageVolume = 0.5f;

    [SerializeField] private AudioClip _explosionAudioClip;
    [SerializeField, Range(0f, 1f)] private float _explosionVolume = 1f;

    public void PlayHitSound() => AudioSource.PlayClipAtPoint(_damageAudioClip, Camera.main.transform.position, _damageVolume);
    public void PlayExplosionSound() => AudioSource.PlayClipAtPoint(_explosionAudioClip, Camera.main.transform.position, _explosionVolume);
    public void PlayHealthSound() => AudioSource.PlayClipAtPoint(_bonusHealthAudioClip, Camera.main.transform.position, _healthBonusVolume);

}
