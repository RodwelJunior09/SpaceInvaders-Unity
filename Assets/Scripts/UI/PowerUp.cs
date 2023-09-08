using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] GameObject itemBox;
    [SerializeField] GameObject itemBoxPos;

    public void ShowItemPowerUp(){
        Instantiate(itemBox, itemBoxPos.transform.position, itemBoxPos.transform.rotation);
    }
}
