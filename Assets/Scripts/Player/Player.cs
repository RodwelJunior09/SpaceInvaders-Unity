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

    // Start is called before the first frame update
    void Start()
    {
        _alertBuffMessage.enabled = false;
        gameStatus = FindObjectOfType<GameStatus>();
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
        if (gameStatus.GetScore() > 500 && !_recievedBonusHealth)
        {
            health += 200;
            _soundManager.PlayHealthSound();
            _recievedBonusHealth = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var damageDealer = collision.GetComponent<DamageDealer>();
        if (!damageDealer)
            return;
        HitProcess(damageDealer);
    }

    private void HitProcess(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
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
                break;
        }
        StartCoroutine(ShowAlertBuffMessage(buffName));
    }

    void IncreaseHealth(){
        health += 100;
    }

    void IncreaseDefense(){
        FindObjectOfType<Enemy>().GetEnemyProjectile.DecreaseDamage(5);
    }

    void IncreaseAttack(){
        GetComponent<Shooting>().GetProjectile.IncreaseDamage(10);
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
    }

    private void Die()
    {
        Destroy(gameObject);
        GameObject vfxExplosion = Instantiate(_explosionGameObject, transform.position, transform.rotation);
        _soundManager.PlayExplosionSound();
        Destroy(vfxExplosion, durationOfExplosion);
        // Loading game over screen...
        FindObjectOfType<Advertising>().ShowInterAd(); // Loads a new inter ads....
        FindObjectOfType<Level>().LoadGameOver(); // Loads a game over screen...
    }

}
