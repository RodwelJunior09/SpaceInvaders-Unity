using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Config Params
    [SerializeField] float speedOfPlayer = 10f;
    [SerializeField] float _padding = 1f;
    [SerializeField] private GameObject _playerLaser;
    [SerializeField] private float projectileSpeed = 1f;
    [SerializeField] private float projectFirePeriod = 1f;

    private Coroutine _fireCoroutine;

    // Local Variables
    private float _xMin;
    private float _xMax;
    private float _yMin;
    private float _yMax;

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
            yield return new WaitForSeconds(projectFirePeriod);
        }
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
