using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUpItem : MonoBehaviour
{
    [SerializeField] Sprite[] _allPowerUpsSprites;

    GameStatus _gameStatus;
    SpriteRenderer item_viewer;

    string powerUpName;

    void Awake(){
        _gameStatus = FindObjectOfType<GameStatus>();
    }

    void OnEnable(){
        item_viewer = GetComponentInChildren<SpriteRenderer>();
    }

    void Start(){
        ShowPowerUp();
    }

    void ShowPowerUp()
    {
        if (_gameStatus.GetScore() % 150 == 0)
        {
            string[] namePowerUps = { "Defense", "Attack", "Health" };
            DisplayPowerType(namePowerUps[Random.Range(0, namePowerUps.Length)]);
        }
    }

    void DisplayPowerType(string namePower)
    {
        switch (namePower.ToLower())
        {
            case "health":
                powerUpName = namePower.ToLower();
                item_viewer.sprite = _allPowerUpsSprites[0];
                break;
            case "defense":
                powerUpName = namePower.ToLower();
                item_viewer.sprite = _allPowerUpsSprites[1];
                break;
            case "attack":
                powerUpName = namePower.ToLower();
                item_viewer.sprite = _allPowerUpsSprites[2];
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.GetComponent<Player>())
        {
            collision.gameObject.GetComponent<Player>().BuffsToPlayer(powerUpName);
            Destroy(this.gameObject);
        }
    }
}
