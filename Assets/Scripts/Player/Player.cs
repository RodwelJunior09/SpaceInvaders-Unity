using System.Collections;
using TMPro;
using UI;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [Header("Player General Config")]
        [SerializeField] private int health = 300;
        [SerializeField] private float durationOfExplosion;
        [SerializeField] private GameObject _explosionGameObject;

        [Header("Player UI")]
        [SerializeField] TextMeshProUGUI _alertBuffMessage;

        private GameStatus _gameStatus;
        private SoundsManager _soundManager;
        private bool _hasBonusHealth;
        private bool _isDead;
        private int _damageReduction;
        private Coroutine _alertCoroutine;

        // Start is called before the first frame update
        private void Start()
        {
            _alertBuffMessage.enabled = false;
            _gameStatus = FindFirstObjectByType<GameStatus>();
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
            if (!_hasBonusHealth && _gameStatus && _gameStatus.GetScore() > 500)
            {
                health += 200;
                _soundManager.PlayHealthSound();
                _hasBonusHealth = true;
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

        private void IncreaseHealth(){
            health += 100;
        }

        private void IncreaseDefense(){
            _damageReduction += 5;
        }

        private void IncreaseAttack(){
            GetComponent<Shooting>().IncreaseAttack(10);
        }

        private IEnumerator ShowAlertBuffMessage(string alertMessage)
        {
            _alertBuffMessage.enabled = true;
            _alertBuffMessage.text = alertMessage.ToLower() switch
            {
                "health" => "Ship health increased!!",
                "defense" => "Ship defense increased!!",
                "attack" => "Ship attack power increased!!",
                _ => _alertBuffMessage.text
            };
            yield return new WaitForSeconds(5);
            _alertBuffMessage.enabled = false;
            _alertCoroutine = null;
        }

        private void Die()
        {
            // Destroy is deferred, so block a second hit in the same frame from killing us twice
            _isDead = true;
            Destroy(gameObject);
            var vfxExplosion = Instantiate(_explosionGameObject, transform.position, transform.rotation);
            _soundManager.PlayExplosionSound();
            Destroy(vfxExplosion, durationOfExplosion);
            // Load the game over screen first so a missing ad object can't block it
            FindFirstObjectByType<Level>().LoadGameOver();
            var advertising = FindFirstObjectByType<Advertising>();
            if (advertising) advertising.ShowInterAd();
        }

    }
}
