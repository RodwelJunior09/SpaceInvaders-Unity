using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int playerDamage = 100;
    [SerializeField] private int enemyDamage = 100;
    public int GetDamage()
    {
        if (this.gameObject.name.Contains("Enemy"))
            return enemyDamage;
        else
            return playerDamage;
    }

    public void IncreaseDamage(int increaseAmount){
        playerDamage += increaseAmount;
    }

    public void DecreaseDamage(int decreaseAmount){
        enemyDamage -= decreaseAmount;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
