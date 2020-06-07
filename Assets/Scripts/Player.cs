using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Config Params
    [Header("Player General Config")]
    [SerializeField] float _padding = 1f;
    [SerializeField] private int health = 300;
    [SerializeField] float speedOfPlayer = 10f;
    [SerializeField] private float durationOfExplosion;
    [SerializeField] private GameObject _explosionGameObject;

    [Header("Projectile Config")]
    [SerializeField] private GameObject _playerLaser;
    [SerializeField] private float projectileSpeed = 1f;
    [SerializeField] private float projectFirePeriod = 1f;

    [Header("Player Sounds")]
    [SerializeField] private AudioClip _bonusHealthAudioClip;
    [SerializeField, Range(0f, 1f)] private float _healthBonusVolume = 1f;

    [SerializeField] AudioClip _laserAudioClips;
    [SerializeField, Range(0f, 1f)] private float _laserVolume = 1f;

    [SerializeField] private AudioClip _damageAudioClip;
    [SerializeField, Range(0f, 1f)] private float _damageVolume = 0.5f;

    [SerializeField] private AudioClip _explosionAudioClip;
    [SerializeField, Range(0f, 1f)] private float _explosionVolume = 1f;

    private Coroutine _fireCoroutine;
    private GameStatus gameStatus;

    // Local Variables
    private float _xMin;
    private float _xMax;
    private float _yMin;
    private float _yMax;

    private bool _recievedBonusHealth = false;

    // Start is called before the first frame update
    void Start()
    {
        SetupMoveBoundaries();
        gameStatus = FindObjectOfType<GameStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
        BonusHealth();
    }

    public int GetPlayerHealth()
    {
        return health;
    }

    private void BonusHealth()
    {
        if (gameStatus.GetScore() > 500 && !_recievedBonusHealth)
        {
            health += 200;
            AudioSource.PlayClipAtPoint(_bonusHealthAudioClip, Camera.main.transform.position, _healthBonusVolume);
            _recievedBonusHealth = true;
        }
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _fireCoroutine = StartCoroutine(ContinousFire());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(_fireCoroutine);   
        }
    }

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var damageDealer = collision.GetComponent<DamageDealer>();
        if (!damageDealer)
        {
            return;
        }
        HitProcess(damageDealer);
    }

    private void HitProcess(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        AudioSource.PlayClipAtPoint(_damageAudioClip, Camera.main.transform.position, _damageVolume);
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        GameObject vfxExplosion = Instantiate(_explosionGameObject, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(_explosionAudioClip, Camera.main.transform.position, _explosionVolume);
        Destroy(vfxExplosion, durationOfExplosion);
        FindObjectOfType<Level>().LoadGameOver();
    }

    private void Move()
    {
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * speedOfPlayer;
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * speedOfPlayer;
        var newXpos = Mathf.Clamp(transform.position.x + deltaX, _xMin, _xMax);
        var newYpos = Mathf.Clamp(transform.position.y + deltaY, _yMin, _yMax);
        transform.position = new Vector2(newXpos, newYpos);
    }

    private void SetupMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        _xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + _padding;
        _xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - _padding;
        _yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + _padding;
        _yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - _padding;
    }

}
