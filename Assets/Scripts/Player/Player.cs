using TMPro;
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [Header("Player General Config")]
    [SerializeField] private int health = 300;
    [SerializeField] private float durationOfExplosion;
    [SerializeField] private GameObject _explosionGameObject;

    [Header("Player UI")]
    [SerializeField] TextMeshProUGUI _alertBuffMessage;

    private GameStatus gameStatus;
    private SoundsManager _soundManager;
    private bool _recievedBonusHealth = false;
    private bool _isDead;
    private int _damageReduction;
    private Coroutine _alertCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        _alertBuffMessage.enabled = false;
        gameStatus = FindFirstObjectByType<GameStatus>();
        _soundManager = GetComponent<SoundsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        BonusHealth();
    }

    public int GetPlayerHealth()
    {
        return health;
    }

    private void BonusHealth()
    {
        if (!_recievedBonusHealth && gameStatus && gameStatus.GetScore() > 500)
        {
            health += 200;
            _soundManager.PlayHealthSound();
            _recievedBonusHealth = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isDead) return;
        var damageDealer = collision.GetComponent<DamageDealer>();
        if (!damageDealer)
            return;
        HitProcess(damageDealer);
    }

    private void HitProcess(DamageDealer damageDealer)
    {
        // Defense buffs reduce each hit, but a hit always does at least 1 damage
        health -= Mathf.Max(1, damageDealer.GetDamage() - _damageReduction);
        _soundManager.PlayHitSound();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    public void BuffsToPlayer(string buffName){
        switch (buffName.ToLower())
        {
            case "health":
                IncreaseHealth();
                break;
            case "defense":
                IncreaseDefense();
                break;
            case "attack":
                IncreaseAttack();
                break;
            default:
                Debug.LogWarning("No buff has been named....");
                return;
        }
        if (_alertCoroutine != null) StopCoroutine(_alertCoroutine);
        _alertCoroutine = StartCoroutine(ShowAlertBuffMessage(buffName));
    }

    void IncreaseHealth(){
        health += 100;
    }

    void IncreaseDefense(){
        _damageReduction += 5;
    }

    void IncreaseAttack(){
        GetComponent<Shooting>().IncreaseAttack(10);
    }

    IEnumerator ShowAlertBuffMessage(string alertMessage)
    {
        _alertBuffMessage.enabled = true;
        switch (alertMessage.ToLower())
        {
            case "health":
                _alertBuffMessage.text = "Ship health increased!!";
                break;
            case "defense":
                _alertBuffMessage.text = "Ship defense increased!!";
                break;
            case "attack":
                _alertBuffMessage.text = "Ship attack power increased!!";
                break;
        }
        yield return new WaitForSeconds(5);
        _alertBuffMessage.enabled = false;
        _alertCoroutine = null;
    }

    private void Die()
    {
        // Destroy is deferred, so block a second hit in the same frame from killing us twice
        _isDead = true;
        Destroy(gameObject);
        GameObject vfxExplosion = Instantiate(_explosionGameObject, transform.position, transform.rotation);
        _soundManager.PlayExplosionSound();
        Destroy(vfxExplosion, durationOfExplosion);
        // Load the game over screen first so a missing ad object can't block it
        FindFirstObjectByType<Level>().LoadGameOver();
        var advertising = FindFirstObjectByType<Advertising>();
        if (advertising) advertising.ShowInterAd();
    }

}
