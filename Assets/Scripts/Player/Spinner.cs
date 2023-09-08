using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] private float speedOfSpin = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, speedOfSpin * Time.deltaTime);
    }
}
