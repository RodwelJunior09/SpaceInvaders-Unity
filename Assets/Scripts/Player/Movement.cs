using UnityEngine;
using UnityEngine.EventSystems;

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

    Joystick joystick;

    // Start is called before the first frame update
    void Start()
    {
        SetupMoveBoundaries();
        joystick = FindObjectOfType<Joystick>();
    }

    // // Update is called once per frame
    void Update()
    {
        //JoystickMovement();
        TouchMovement();
    }

    private void FixedUpdate()
    {
        //TouchMovement();
    }

    void TouchMovement()
    {

        for (int i = 0; i < Input.touchCount; i++)
        {
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.touches[i].position);

            this.transform.position = new Vector2(touchPos.x, touchPos.y);
        }
    }

    void JoystickMovement(){
        var deltaY = joystick.Vertical * Time.deltaTime * speedOfPlayer;
        var deltaX = joystick.Horizontal * Time.deltaTime * speedOfPlayer;

        var newXpos = Mathf.Clamp(transform.position.x + deltaX, _xMin, _xMax);
        var newYpos = Mathf.Clamp(transform.position.y + deltaY, _yMin, _yMax);
        transform.position = new Vector2(newXpos, newYpos);
    }

    private void MovePlayer(){
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * speedOfPlayer;
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * speedOfPlayer;
        var newXpos = Mathf.Clamp(transform.position.x + deltaX, _xMin, _xMax);
        var newYpos = Mathf.Clamp(transform.position.y + deltaY, _yMin, _yMax);
        transform.position = new Vector2(newXpos, newYpos);
    }

    private void SetupMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        _xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + _Xpadding;
        _xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - _Xpadding;
        _yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + _Ypadding;
        _yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - _Ypadding;
    }
}
