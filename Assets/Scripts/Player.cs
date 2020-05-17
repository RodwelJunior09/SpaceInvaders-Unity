using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Config Params
    [SerializeField] float speedOfPlayer = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] GameObject playerLaser;
    [SerializeField] private float projectileSpeed = 1f;
    [SerializeField] private float projectFirePeriod = 1f;

    private Coroutine firingCourutine;

    // Local Variables
    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;

    // Start is called before the first frame update
    void Start()
    {
        SetupMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }


    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCourutine = StartCoroutine(ContinousFire());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCourutine);   
        }
    }

    private IEnumerator ContinousFire()
    {
        while (true)
        {
            var laser = Instantiate(playerLaser, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            yield return new WaitForSeconds(projectFirePeriod);
        }
    }

    private void Move()
    {
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * speedOfPlayer;
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * speedOfPlayer;
        var newXpos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYpos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXpos, newYpos);
    }

    private void SetupMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

}
