using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float _Ypadding = 1f;
    [SerializeField] float _Xpadding = 1f;
    [SerializeField, Range(1f, 5f)] float speedOfPlayer = 2f;

    // Local Variables
    private float _xMin;
    private float _xMax;
    private float _yMin;
    private float _yMax;

    private Camera _gameCamera;

    // Start is called before the first frame update
    void Start()
    {
        _gameCamera = Camera.main;
        SetupMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        // Keyboard fallback so the game can be played in the Editor
        if (Input.touchCount > 0) TouchMovement();
        else MovePlayer();
    }

    void TouchMovement()
    {
        // Follow the most recent touch
        Vector3 touchPos = _gameCamera.ScreenToWorldPoint(Input.GetTouch(Input.touchCount - 1).position);
        SetClampedPosition(touchPos.x, touchPos.y);
    }

    private void MovePlayer(){
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * speedOfPlayer;
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * speedOfPlayer;
        SetClampedPosition(transform.position.x + deltaX, transform.position.y + deltaY);
    }

    private void SetClampedPosition(float x, float y)
    {
        transform.position = new Vector2(Mathf.Clamp(x, _xMin, _xMax), Mathf.Clamp(y, _yMin, _yMax));
    }

    private void SetupMoveBoundaries()
    {
        _xMin = _gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + _Xpadding;
        _xMax = _gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - _Xpadding;
        _yMin = _gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + _Ypadding;
        _yMax = _gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - _Ypadding;
    }
}
